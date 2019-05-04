
namespace BookInspector.DATA
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using BookInspector.DATA.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DbUser> DBUsers { get; set; }
        public DbSet<FavoriteBook> FavoriteBooks { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<UserBookRating> UserBookRating { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces()
                    .Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);

                builder.ApplyConfiguration(configurationInstance);
            }

            base.OnModelCreating(builder);
        }
    }
}
