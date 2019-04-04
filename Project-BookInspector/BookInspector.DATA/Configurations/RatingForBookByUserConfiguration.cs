namespace BookInspector.Data.Configurations
{
    using BookInspector.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RatingForBookByUserConfiguration : IEntityTypeConfiguration<RatingForBookByUser>
    {

        public void Configure(EntityTypeBuilder<RatingForBookByUser> builder)
        {
            builder
                .HasKey(rb => new { rb.BookId, rb.UserId });

            builder
            .HasOne(rb => rb.Book)
            .WithMany(b => b.RatingByBook)
            .HasForeignKey(rb => rb.BookId);

            builder
                .HasOne(rb => rb.User)
                .WithMany(u => u.BookRatings)
                .HasForeignKey(rb => rb.BookId);


            builder
                .Property(rb => rb.Rating)
                .IsRequired(); 

        }
    }
}
