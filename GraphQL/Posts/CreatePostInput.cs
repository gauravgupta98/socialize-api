using System;

namespace socialize_api.GraphQL.Posts
{
    /// <summary>
    /// Record for creating post input.
    /// </summary>
    public record CreatePostInput(string PostData, Guid UserId);
}