
namespace BookInspector.Data.Configurations
{
    using BookInspector.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.PublisherId)
                .IsRequired();

            builder.Property(x => x.PublishedDate)
                .IsRequired();

            builder.Property(x => x.Isbn)
                .HasMaxLength(13)
                .IsRequired();

            builder.Property(x => x.PageCount)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();
        }
    }
}
