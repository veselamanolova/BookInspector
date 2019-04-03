﻿
namespace BookInspector.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }

        public string PublisherId { get; set; }

        public DateTime PublishedDate { get; set; }

        public int Isbn { get; set; }

        public int? VolumeId { get; set; }

        public int? PageCount { get; set; }

        public string Description { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<FavoriteBook> FavoriteBook { get; set; }
        public virtual ICollection<RatingByBook> RatingByBook { get; set; }
        public virtual ICollection<BookByCategory> BookByCategory { get; set; }
        public virtual ICollection<BookByAuthor> BookByAuthor { get; set; }
    }
}
