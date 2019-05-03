namespace BookInspector.DATA.Models
{
    public class UserBookRating
    {
        public int BookId { get; set; }

        public string DbUserId { get; set; }

        public int Rating { get; set; }

        public Book Book { get; set; }

        public DbUser User { get; set; }


    }
}
