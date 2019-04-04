
using System;

namespace BookInspector.Data.Context
{
    using System;
    using System.Linq;
    using System.Reflection;
    using BookInspector.Data.Models;
    using Microsoft.EntityFrameworkCore;
    
    public class BookInspectorContext : DbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookByAuthor> BookByAuthor { get; set; }
        public DbSet<BookByCategory> BookByCategory { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<FavoriteBook> FavoriteBook { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<RatingForBookByUser> RatingByBook { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString =
              //   @"Server=DESKTOP-OHKJ4C7\SQLEXPRESS;Database=BookInspector;Trusted_Connection=True;";
              @"Server=localhost\SQLEXPRESS;Database=BookInspector;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }

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

            /*
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteBookConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookByCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BookByAuthorConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());       
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RatingForBookByUserConfiguration());
            */

            base.OnModelCreating(modelBuilder);
        }
    }
}
