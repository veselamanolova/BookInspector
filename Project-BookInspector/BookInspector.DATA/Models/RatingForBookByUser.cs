
namespace BookInspector.Data.Models
{
    public class RatingForBookByUser
    {
        public int RatingId { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }
    }
}