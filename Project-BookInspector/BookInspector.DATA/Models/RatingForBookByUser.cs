
namespace BookInspector.Data.Models
{
    public class RatingForBookByUser
    {
        public int BookId { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }

        public Book Book { get; set; }

        public User User { get; set; }
    }
}