
namespace BookInspector.DATA.Models
{
    public class FavoriteBook
    {
        public string UserId { get; set; }

        public int BookId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Book Book { get; set; }
    }
}