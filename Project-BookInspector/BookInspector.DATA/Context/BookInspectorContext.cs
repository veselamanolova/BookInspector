
namespace BookInspector.Data.Context
{
    using System;
    using System.Linq;
    using System.Reflection;
    using BookInspector.Data.Models;
    using Microsoft.EntityFrameworkCore;
    
    public class BookInspectorContext : DbContext, IBookInspectorContext
    {
        public BookInspectorContext(DbContextOptions options)
            :base(options) { }

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookByAuthor> BookByAuthor { get; set; }
        public DbSet<BookByCategory> BookByCategory { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<FavoriteBook> FavoriteBook { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<RatingForBookByUser> RatingByBook { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces()
                    .Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();
            
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);

                modelBuilder.ApplyConfiguration(configurationInstance);
            }
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
