
namespace BookInspector.DATA.Configurations
{
    using BookInspector.DATA.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BookByCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            builder
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BooksCategories)
                .HasForeignKey(bc => bc.BookId)
                .IsRequired();

            builder
                .HasOne(bc => bc.Category)
                .WithMany(a => a.BookCategory)
                .HasForeignKey(bc => bc.CategoryId)
                .IsRequired();
        }
    }
}
