namespace socialize_api.GraphQL.Users
{
    /// <summary>
    /// Record for loggin in a user.
    /// </summary>
    public record LoginInput(string Email, string Password);
}