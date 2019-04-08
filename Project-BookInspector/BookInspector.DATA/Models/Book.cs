
namespace BookInspector.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }        

        public int PublisherId { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Isbn { get; set; }

        public int? VolumeId { get; set; }

        public int? PageCount { get; set; }

        public string Description { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<FavoriteBook> FavoriteBook { get; set; }
        public virtual ICollection<RatingForBookByUser> RatingByBook { get; set; }
        public virtual ICollection<BookByCategory> BookByCategory { get; set; }
        public virtual ICollection<BookByAuthor> BookByAuthor { get; set; }
    }
}
