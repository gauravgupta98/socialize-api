using socialize_api.Data;
using System.Threading.Tasks;

namespace socialize_api.Services
{
    /// <summary>
    /// Interface for the authentication service.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticates the User.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="context">The db context.</param>
        /// <returns>Exception if not authenticated, otherwise JWT Token.</returns>
        string Authenticate(string email, string password, AppDbContext context);
    }
}
