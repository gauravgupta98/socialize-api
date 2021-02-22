using HotChocolate;
using HotChocolate.Types;
using socialize_api.Models;

namespace socialize_api.GraphQL
{
    /// <summary>
    /// GraphQL Subscriptions.
    /// </summary>
    public class Subscription
    {
        #region Methods
        /// <summary>
        /// Subscription which gets called when a new user is created.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <returns>The user.</returns>
        [Subscribe]
        [Topic]
        public User OnUserAdded([EventMessage] User user) => user;
        #endregion Methods
    }
}