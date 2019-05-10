
namespace BookInspector.Models.Catalog
{
    using System;
    using System.Collections.Generic;

    public class DetailsIndexModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public string PreviewLink { get; set; }

        public IEnumerable<string> Authors { get; set; }

        public IEnumerable<string> Categories { get; set; }
    }
}
