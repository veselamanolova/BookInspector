
using BookInspector.DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
        builder
            .HasKey(rb => new { rb.BookId, rb.AuthorId });

        builder
            .HasOne(rb => rb.Book)
            .WithMany(b => b.BooksAuthors)
            .HasForeignKey(rb => rb.BookId);

        builder
            .HasOne(rb => rb.Author)
            .WithMany(a => a.BooksAuthors)
            .HasForeignKey(rb => rb.AuthorId);
    }
}