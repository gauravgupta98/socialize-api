using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using socialize_api.Data;
using socialize_api.Models;

namespace socialize_api.GraphQL.Posts
{
    /// <summary>
    /// The Post Type.
    /// </summary>
    public class PostType : ObjectType<Post>
    {
        #region Methods
        /// <summary>
        /// The configure method for Post type. 
        /// </summary>
        /// <param name="descriptor">The post descriptor.</param>
        protected override void Configure(IObjectTypeDescriptor<Post> descriptor)
        {
            descriptor.Description("Represents the posts which users have added.");

            descriptor
                .Field(p => p.User)
                .ResolveWith<Resolvers>(p => p.GetUser(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("The user to which the post belong.");
        }
        #endregion Methods

        /// <summary>
        /// Resolvers for Post.
        /// </summary>
        private class Resolvers
        {
            #region Methods
            /// <summary>
            /// Gets the user which is linked to a post.
            /// </summary>
            /// <param name="post">The post object.</param>
            /// <param name="context">The db context.</param>
            /// <returns>The user associated with the post.</returns>
            public User GetUser(Post post, [ScopedService] AppDbContext context) =>
                context.Users.FirstOrDefault(p => p.Id == post.UserId);
            #endregion Methods
        }
    }
}