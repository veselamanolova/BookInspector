
namespace BookInspector.DATA
{
    using System.Collections.Generic;

    public class Author
    {
        public Author()
        {
            BookByAuthor = new HashSet<BookByAuthor>();
        }

        public int AuthorId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BookByAuthor> BookByAuthor { get; set; }
    }
}