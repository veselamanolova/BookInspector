
namespace BookInspector.DATA.Models
{
    public class UserBookRating
    {
        public int BookId { get; set; }

        public string UserId { get; set; }

        public int Rating { get; set; }

        public Book Book { get; set; }

        public ApplicationUser User { get; set; }
    }
}
