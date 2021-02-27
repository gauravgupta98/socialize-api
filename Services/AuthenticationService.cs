using Microsoft.IdentityModel.Tokens;
using socialize_api.Data;
using socialize_api.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace socialize_api.Services
{
    /// <summary>
    /// Service for Authentication of the API.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        #region Methods
        /// <summary>
        /// Authenticates the User.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="context">The db context.</param>
        /// <returns>Exception if not authenticated, otherwise JWT Token.</returns>
        public string Authenticate(string email, string password, AppDbContext context)
        {
            User user = context.Users.FirstOrDefault(user => user.Email == email);

            if (user == null)
                throw new InvalidOperationException("Error! User doesn't exist with provided credentials.");

            if (!PasswordService.Validate(user.PasswordSalt, user.PasswordHash, password))
                throw new InvalidOperationException("Oops! Invalid credentials. Please enter valid credentials.");

            return GenerateAccessToken(user.Email, user.Id);
        }

        /// <summary>
        /// Generates the access token.
        /// </summary>
        /// <param name="email">The user email.</param>
        /// <param name="userId">The id of the user.</param>
        /// <returns>The token.</returns>
        private string GenerateAccessToken(string email, Guid userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("$3cr3+SecretKEY3333"));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, email)
            };

            var token = new JwtSecurityToken(
                "issuer",
                "audience",
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion Methods
    }
}
