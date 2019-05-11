
namespace BookInspector.Models.Catalog
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SearchViewModel
    {
        [Required]
        public string Key;

        public IEnumerable<CatalogListingModel> BooksList { get; set; }
    }
}
