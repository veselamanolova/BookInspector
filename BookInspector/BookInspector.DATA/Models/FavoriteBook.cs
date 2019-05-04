namespace BookInspector.DATA.Models
{
    public class FavoriteBook
    {
        public string DbUserId { get; set; }

        public int BookId { get; set; }

        public virtual DbUser User { get; set; }

        public virtual Book Book { get; set; }
    }
}