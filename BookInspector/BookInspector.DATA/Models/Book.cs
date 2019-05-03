
namespace BookInspector.DATA.Models
{
    using System;
    using System.Collections.Generic;

    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PublisherId { get; set; }

        public DateTime PublishedDate { get; set; }

        public string ImageURL { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual Category Category { get; set; }

        public virtual IEnumerable<BookAuthor> Authors { get; set; }

        public object Select()
        {
            throw new NotImplementedException();
        }
    }
}

