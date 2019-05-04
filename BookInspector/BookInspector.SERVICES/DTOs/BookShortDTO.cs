using BookInspector.DATA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookInspector.SERVICES.DTOs
{
    public class BookShortDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishedDate { get; set; }

        public string ImageURL { get; set; }     

        public string ShortDescription { get; set; }

        public string PublisherName { get; set;  }

        public ICollection<string> AuthorNames { get; set; }

    }
}
