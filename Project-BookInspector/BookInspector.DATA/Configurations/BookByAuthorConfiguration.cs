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
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            builder
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookByAuthor)
                .HasForeignKey(ba => ba.BookId);

            builder
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookByAuthor)
                .HasForeignKey(ba => ba.BookId);
        }
    }
}

