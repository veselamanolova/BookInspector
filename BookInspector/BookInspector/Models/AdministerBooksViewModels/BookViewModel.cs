namespace BookInspector.Models.AdministerBooksViewModels
{
    using BookInspector.Models.Catalog;
    
    public class BookViewModel: DetailsIndexModel
    {
        public string Isbn { get; set; }

        public string ShortDescription { get; set; }
    }
}
