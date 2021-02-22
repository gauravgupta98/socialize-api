using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using socialize_api.Data;
using socialize_api.Models;

namespace socialize_api.GraphQL
{
    /// <summary>
    /// GraphQL Queries.
    /// </summary>
    public class Query
    {
        #region Methods
        /// <summary>
        /// Gets the users from the database.
        /// UseDbContext is used to run multiples queries in parallel. Multi-threading.
        /// </summary>
        /// <param name="context">The db context.</param>
        /// <returns>The users collection.</returns>
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<User> GetUser([ScopedService] AppDbContext context) => context.Users;

        /// <summary>
        /// Gets the posts from the database.
        /// UseDbContext is used to run multiples queries in parallel. Multi-threading.
        /// </summary>
        /// <param name="context">The db context.</param>
        /// <returns>The posts collection.</returns>
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Post> GetPosts([ScopedService] AppDbContext context) => context.Posts;
        #endregion Methods
    }
}