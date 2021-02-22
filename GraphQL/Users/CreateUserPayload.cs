using socialize_api.Models;

namespace socialize_api.GraphQL.Users
{
    /// <summary>
    /// Record for create user payload.
    /// </summary>
    public record CreateUserPayload(User user);
}