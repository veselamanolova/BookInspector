namespace BookInspector.DATA.Models
{    
    using System;
    using System.Collections.Generic;

    public class Book
    {
        public Book()
        {
            BooksCategories = new List<BookCategory>();
            BooksAuthors = new List<BookAuthor>(); 

            
        }
        public int Id { get; set; }

        public string Title { get; set; }           

        public DateTime PublishedDate { get; set; }

        public string ImageURL { get; set; }

        public string Isbn { get; set; }

        public string Description { get; set; }

        //textSnippet
        public string ShortDescription { get; set; }

        public string PreviewLink { get; set; }

        public int PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual IEnumerable<BookCategory> BooksCategories { get; set; }

        public virtual ICollection<BookAuthor> BooksAuthors { get; set; }

        public virtual IEnumerable<FavoriteBook> FavoriteBooks { get; set; }

        public virtual IEnumerable<UserBookRating> BooksRatings { get; set; }

        public object Select()
        {
            throw new NotImplementedException();
        }
    }
}

