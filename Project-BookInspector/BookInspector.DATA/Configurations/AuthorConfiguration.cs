
namespace BookInspector.Data.Configurations
{
    using BookInspector.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {           
            builder.Property(a => a.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}

// var books = context.Books.ToList();
// var books = context.Books.Include(b => b.Author).ToList(); 