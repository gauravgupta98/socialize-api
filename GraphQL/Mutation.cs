using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using socialize_api.Data;
using socialize_api.GraphQL.Posts;
using socialize_api.GraphQL.Users;
using socialize_api.Models;
using socialize_api.Services;

namespace socialize_api.GraphQL
{
    /// <summary>
    /// GraphQL Mutations.
    /// </summary>
    public class Mutation
    {
        #region Methods
        /// <summary>
        /// Tries to login the user and return authentication token.
        /// </summary>
        /// <param name="credentials">The login credentials.</param>
        /// <param name="context">The db context.</param>
        /// <param name="authService">The authentication service.</param>
        /// <returns>Exception if not authenticated, else auth token.</returns>
        [UseDbContext(typeof(AppDbContext))]
        public LoginPayload Login(LoginInput credentials, [ScopedService] AppDbContext context, [Service] IAuthenticationService authService) =>
            new LoginPayload(authService.Authenticate(credentials.Email, credentials.Password, context));

        /// <summary>
        /// Creates new user.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <param name="context">The db context.</param>
        /// <param name="eventSender">The event sender (used for subscriptions)</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new user payload.</returns>
        [UseDbContext(typeof(AppDbContext))]
        public async Task<CreateUserPayload> CreateUserAsync(CreateUserInput userInput, [ScopedService] AppDbContext context, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
        {
            User user = context.Users.FirstOrDefault(user => user.Email == userInput.Email);
            if (user != null)
            {
                throw new ApplicationException("Oops! A user with same email already exists.");
            }

            string salt = PasswordService.GenerateSalt();
            
            user = new User
            {
                Name = userInput.Name,
                Email = userInput.Email,
                PasswordSalt = salt,
                PasswordHash = PasswordService.GeneratePasswordHash(Convert.FromBase64String(salt), userInput.Password),
                CreatedTime = DateTime.Now
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            await eventSender.SendAsync(nameof(Subscription.OnUserAdded), user, cancellationToken);

            return new CreateUserPayload(user);
        }

        /// <summary>
        /// Creates new post
        /// </summary>
        /// <param name="postData">The post content.</param>
        /// <param name="context">The user content.</param>
        /// <returns>The new post payload.</returns>
        [Authorize]
        [UseDbContext(typeof(AppDbContext))]
        public async Task<CreatePostPayload> CreatePostAsync(CreatePostInput postData, [ScopedService] AppDbContext context)
        {
            Post post = new Post
            {
                PostData = postData.PostData,
                UserId = postData.UserId,
                CreatedTime = DateTime.Now
            };

            context.Posts.Add(post);
            await context.SaveChangesAsync();

            return new CreatePostPayload(post);
        }
        #endregion Methods
    }
}