
namespace BookInspector.Web.Mappers
{
    using BookInspector.Web.Models;
    using BookInspector.Data.Models;
    using BookInspector.Web.Mappers.Contracts;

    public class ViewModelMapper : IViewModelMapper<Book, BookViewModel>
    {
        public BookViewModel MapFrom(Book entity)
             => new BookViewModel
             {
                 Title = entity.Title
             };
    }
}
