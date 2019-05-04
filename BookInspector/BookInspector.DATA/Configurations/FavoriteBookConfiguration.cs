
namespace BookInspector.Data.Configurations
{
    using BookInspector.DATA.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class FavoriteBookConfiguration : IEntityTypeConfiguration<FavoriteBook>
    {
        public void Configure(EntityTypeBuilder<FavoriteBook> builder)
        {
            builder
                .HasKey(fb => new { fb.DbUserId, fb.BookId });           

            builder
                .HasOne(fb => fb.Book)
                .WithMany(b => b.FavoriteBook)
                .HasForeignKey(fb => fb.BookId)
                .IsRequired();

            builder
               .HasOne(fb => fb.User)
               .WithMany(u => u.FavoriteBook)
               .HasForeignKey(fb => fb.DbUserId)
               .IsRequired();
        }
    }
}