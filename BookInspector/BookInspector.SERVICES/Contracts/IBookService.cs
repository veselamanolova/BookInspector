
namespace BookInspector.SERVICES.Contracts
{
    using BookInspector.DATA.Models;
    using BookInspector.SERVICES.DTOs;
    using System.Collections.Generic;

    public interface IBookService
    {
        Book GetById(int id);

        IEnumerable<Book> GetAll();

        IEnumerable<BookShortDTO> GetShortBooks();

        BookDetailsDTO GetBookDetailsById(int id);
    }
}
