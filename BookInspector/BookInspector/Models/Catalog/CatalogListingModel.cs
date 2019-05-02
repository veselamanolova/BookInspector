
namespace BookInspector.Models.Catalog
{
    using System;

    public class CatalogListingModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Category { get; set; }

        public string ImageURL { get; set; }

        // public virtual IEnumerable<BookAuthor> Authors { get; set; }
    }
}
