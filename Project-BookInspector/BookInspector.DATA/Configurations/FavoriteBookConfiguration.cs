
namespace BookInspector.Data.Configurations
{
    using BookInspector.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class FavoriteBookConfiguration : IEntityTypeConfiguration<FavoriteBook>
    {
        public void Configure(EntityTypeBuilder<FavoriteBook> builder)
        {
            builder
                .HasKey(rb => new {rb.UserId, rb.BookId});

            builder
                .HasOne(rb => rb.User)
                .WithMany(b => b.FavoriteBook)
                .HasForeignKey(rb => rb.UserId)
                .IsRequired();

            builder
                .HasOne(rb => rb.Book)
                .WithMany(a => a.FavoriteBook)
                .HasForeignKey(rb => rb.BookId)
                .IsRequired();
        }
    }
}
