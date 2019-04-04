
namespace BookInspector.Data.Configurations
{
    using BookInspector.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BookByCategoryConfiguration : IEntityTypeConfiguration<BookByCategory>
    {
        public void Configure(EntityTypeBuilder<BookByCategory> builder)
        {
            builder.Property(x => x.BookId)
                .IsRequired();

            builder.Property(x => x.CategoryId)
                .IsRequired();
        }
    }
}
