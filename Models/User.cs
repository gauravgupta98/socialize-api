using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace socialize_api.Models
{
    /// <summary>
    /// The User model.
    /// </summary>
    public class User
    {
        #region Properties
        /// <summary>
        /// Gets or sets the Id of the user.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the user.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Email of the user.
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        [Required]
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the created time of user.
        /// </summary>
        [Required]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the Posts of the user.
        /// </summary>
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        #endregion Properties
    }
}