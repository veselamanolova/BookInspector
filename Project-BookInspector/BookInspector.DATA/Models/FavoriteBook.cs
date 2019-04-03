
namespace BookInspector.Data.Models
{
    public class FavoriteBook
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public virtual User User { get; set; }

        public virtual Book Book { get; set; }
    }
}