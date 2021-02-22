namespace socialize_api.GraphQL.Users
{
    /// <summary>
    /// Record for creating a new user.
    /// </summary>
    public record CreateUserInput(string Name, string Email, string Password);
}