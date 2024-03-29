﻿
namespace BookInspector.Data.Configurations
{
    using BookInspector.DATA.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserBookRatingConfiguration : IEntityTypeConfiguration<UserBookRating>
    {

        public void Configure(EntityTypeBuilder<UserBookRating> builder)
        {
            builder
                .HasKey(rb => new { rb.BookId, rb.UserId });

            builder
            .HasOne(ubr => ubr.Book)
            .WithMany(b => b.BooksRatings)
            .HasForeignKey(ubr => ubr.BookId);

            builder
                .HasOne(ubr => ubr.User)
                .WithMany(u => u.RatingByBook)
                .HasForeignKey(ubr => ubr.UserId);


            builder
                .Property(rb => rb.Rating)
                .IsRequired();
        }
    }
}
