namespace BookInspector.Data.Models.Configurations
{
    using BookInspector.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BookByAuthorConfiguration : IEntityTypeConfiguration<BookByAuthor>
    {
        public void Configure(EntityTypeBuilder<BookByAuthor> builder)
        {
            builder
                .HasKey(rb => new { rb.BookId, rb.AuthorId });

            builder
                .HasOne(rb => rb.Book)
                .WithMany(b => b.BookByAuthor)
                .HasForeignKey(rb => rb.BookId);

            builder
                .HasOne(rb => rb.Author)
                .WithMany(a => a.BookByAuthor)
                .HasForeignKey(rb => rb.AuthorId);
        }
    }
}

