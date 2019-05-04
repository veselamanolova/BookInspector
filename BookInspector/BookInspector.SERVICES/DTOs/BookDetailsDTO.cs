using BookInspector.DATA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookInspector.SERVICES.DTOs
{
    public class BookDetailsDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishedDate { get; set; }

        public string ImageURL { get; set; }

        public string Isbn { get; set; }

        public string Description { get; set; }
        
        public string ShortDescription { get; set; }

        public int PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual IEnumerable<BookCategory> BookCategories { get; set; }

        public virtual IEnumerable<BookAuthor> Authors { get; set; }

        public virtual IEnumerable<FavoriteBook> FavoriteBook { get; set; }

        public virtual IEnumerable<UserBookRating> RatingByBook { get; set; }

    }
}
