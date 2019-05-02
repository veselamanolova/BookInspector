
namespace BookInspector.Models.Catalog
{
    using System.Collections.Generic;

    public class CatalogIndexModel
    {
        public IEnumerable<CatalogListingModel> BooksList { get; set; }
    }
}
