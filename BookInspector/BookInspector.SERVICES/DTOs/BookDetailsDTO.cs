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

        public string PublisherName { get; set; }

        public string PreviewLink { get; set; }

        public ICollection<string> AuthorNames { get; set; }

        public ICollection<string> Categories { get; set; }       

    }
}
