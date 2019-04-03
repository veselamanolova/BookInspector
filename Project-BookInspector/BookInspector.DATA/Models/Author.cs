
namespace BookInspector.DATA
{
    using System.Collections.Generic;

    public class Author
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<BookByAuthor> BookByAuthor { get; set; }
    }
}