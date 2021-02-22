using Microsoft.EntityFrameworkCore;
using socialize_api.Models;

namespace socialize_api.Data
{
    /// <summary>
    /// The Database Context class.
    /// </summary>
    public class AppDbContext : DbContext
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The DbContext options.</param>
        public AppDbContext(DbContextOptions options) : base(options) { }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the Users collection.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the Posts collection.
        /// </summary>
        public DbSet<Post> Posts { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// This methods sets the mapping of different entities when models are created.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(user => user.Posts)
                .WithOne(user => user.User!)
                .HasForeignKey(user => user.UserId);

            modelBuilder
                .Entity<Post>()
                .HasOne(post => post.User)
                .WithMany(post => post.Posts)
                .HasForeignKey(post => post.UserId);
        }
        #endregion Methods
    }
}