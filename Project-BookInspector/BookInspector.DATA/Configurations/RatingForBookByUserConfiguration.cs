namespace BookInspector.Data.Configurations
{
    using BookInspector.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    class RatingForBookByUserConfiguration : IEntityTypeConfiguration<RatingForBookByUser>
    {

        public void Configure(EntityTypeBuilder<RatingForBookByUser> builder)
        {
            builder
                .HasKey(rb => new { rb.BookId, rb.UserId }); 

        }
    }
}
