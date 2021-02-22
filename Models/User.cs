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
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Posts of the user.
        /// </summary>
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        #endregion Properties
    }
}