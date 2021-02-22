using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using socialize_api.Data;
using socialize_api.Models;

namespace socialize_api.GraphQL.Users
{
    /// <summary>
    /// The User Type.
    /// </summary>
    public class UserType : ObjectType<User>
    {
        #region Methods
        /// <summary>
        /// The configure method for User type.
        /// </summary>
        /// <param name="descriptor">The user descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Description("Represents the users who have signed up and are using the Socialize API.");

            descriptor
                .Field(p => p.Posts)
                .ResolveWith<Resolvers>(p => p.GetPosts(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("List of posts posted by a user.");
        }
        #endregion Methods

        /// <summary>
        /// Resolvers for User.
        /// </summary>
        private class Resolvers
        {
            #region Methods
            /// <summary>
            /// Gets the list of posts which user has created.
            /// </summary>
            /// <param name="user">The user object.</param>
            /// <param name="context">The db context.</param>
            /// <returns>The list of posts which user has created.</returns>
            public IQueryable<Post> GetPosts(User user, [ScopedService] AppDbContext context) =>
                context.Posts.Where(p => p.UserId == user.Id);
            #endregion Methods
        }
    }
}