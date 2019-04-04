
using System;

namespace BookInspector.Data.Context
{
    using Microsoft.EntityFrameworkCore;
    using BookInspector.Data.Context;
    using BookInspector.Data.Models;
    using BookInspector.Data.Models.Configurations;
    using BookInspector.Data.Configurations;
    using System.Reflection;
    using System.Linq;

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
                @"Server=localhost\SQLEXPRESS;Database=BookInspector;Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            var configurations = Assembly.GetExecutingAssembly()
                .DefinedTypes.Where(
                    typeInfo => typeInfo.ImplementedInterfaces.Contains(typeof(IEntityTypeConfiguration<>)))
                .ToList();
            foreach (var configuration in configurations)
            {
                // var c = Activator.CreateInstance(configuration.AsType()) as IEntityTypeConfiguration<>;

                // modelBuilder.ApplyConfiguration(c);
            }
            */

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
