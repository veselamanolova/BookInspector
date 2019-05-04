
namespace BookInspector.DATA.Models
{
    using System.Collections.Generic;

    public class Author
    {
        public Author()
        {
            BooksAuthors = new List<BookAuthor>(); 
        }
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public virtual IEnumerable<BookAuthor> BooksAuthors { get; set; }
    }
}
