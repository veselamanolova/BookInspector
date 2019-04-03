
namespace BookInspector.Data.Models
{
    public class BookByAuthor
    {
        public int BookId { get; set; }

        public int AuthorId { get; set; }

        public virtual Book Book { get; set; }

        public virtual Author Author { get; set; }
    }
}