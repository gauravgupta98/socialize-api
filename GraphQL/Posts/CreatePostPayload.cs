using socialize_api.Models;

namespace socialize_api.GraphQL.Posts
{
    /// <summary>
    /// Record for creating post payload.
    /// </summary>
    public record CreatePostPayload(Post post);
}