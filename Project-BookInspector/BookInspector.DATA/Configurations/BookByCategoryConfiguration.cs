
namespace BookInspector.Data.Configurations
{
    using BookInspector.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BookByCategoryConfiguration : IEntityTypeConfiguration<BookByCategory>
    {
        public void Configure(EntityTypeBuilder<BookByCategory> builder)
        {
            builder
                .HasKey(rb => new { rb.BookId, rb.CategoryId });

            builder
                .HasOne(rb => rb.Book)
                .WithMany(b => b.BookByCategory)
                .HasForeignKey(rb => rb.BookId)
                .IsRequired();

            builder
                .HasOne(rb => rb.Category)
                .WithMany(a => a.BookByCategory)
                .HasForeignKey(rb => rb.CategoryId)
                .IsRequired();
        }
    }
}
