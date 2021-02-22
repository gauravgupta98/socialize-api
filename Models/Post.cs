using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace socialize_api.Models
{
    /// <summary>
    /// The Post model.
    /// </summary>
    public class Post
    {
        #region Properties
        /// <summary>
        /// Gets or sets the Id of the post.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the content of the post.
        /// </summary>
        [Required]
        public string PostData { get; set; }

        /// <summary>
        /// Gets or sets the Id of the User.
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the User associated with the Post.
        /// </summary>
        public User User { get; set; }
        #endregion Properties
    }
}