
namespace BookInspector.DATA.Models
{
    using System.Collections.Generic;

    public class Author
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public virtual ICollection<BookAuthor> Books { get; set; }
    }
}
