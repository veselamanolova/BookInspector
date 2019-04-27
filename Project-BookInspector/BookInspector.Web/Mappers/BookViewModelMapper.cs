
using System.Linq;
using BookInspector.Web.Models;
using BookInspector.Data.Models;
using BookInspector.Web.Mappers.Contracts;

public class BookViewModelMapper : IBookViewModelMapper<Book, BookViewModel>
{
    public BookViewModel MapFrom(Book entity)
         => new BookViewModel
         {
             _id = entity.BookId,
            _title = entity.Title,
            _ISBN = entity.Isbn,
            _description = entity.Description
            //_author = entity.BookByAuthor.Where(x => entity.BookId == x.AuthorId).Select(x => x.Author.Name).ToList(),
            //_category = entity.BookByCategory.Where(x => entity.BookId == x.CategoryId).Select(x => x.Category.Name).ToList(),
            //_rating = entity.RatingByBook.Select(x => x.Rating).Average()
         };
}
