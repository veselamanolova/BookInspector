
namespace BookInspector.Models.Catalog
{
    using BookInspector.DATA.Models;
    using System.Collections.Generic;

    public class CatalogListingModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string ImageURL { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
