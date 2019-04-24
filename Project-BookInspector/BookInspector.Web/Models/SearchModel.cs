
namespace BookInspector.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SearchModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "Please provide at least 1 letters")]
        public string SearchBook { get; set; }

        public IReadOnlyList<BookViewModel> SearchResults { get; set; } = new List<BookViewModel>();
    }
}
