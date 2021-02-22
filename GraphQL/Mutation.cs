using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using socialize_api.Data;
using socialize_api.GraphQL.Posts;
using socialize_api.GraphQL.Users;
using socialize_api.Models;

namespace socialize_api.GraphQL
{
    /// <summary>
    /// GraphQL Mutations.
    /// </summary>
    public class Mutation
    {
        #region Methods
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
            User user = new User
            {
                Name = userInput.Name,
                Email = userInput.Email,
                Password = userInput.Password
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
        [UseDbContext(typeof(AppDbContext))]
        public async Task<CreatePostPayload> CreatePostAsync(CreatePostInput postData, [ScopedService] AppDbContext context)
        {
            Post post = new Post
            {
                PostData = postData.PostData,
                UserId = postData.UserId
            };

            context.Posts.Add(post);
            await context.SaveChangesAsync();

            return new CreatePostPayload(post);
        }
        #endregion Methods
    }
}