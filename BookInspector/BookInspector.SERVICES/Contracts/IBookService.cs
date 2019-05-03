
namespace BookInspector.SERVICES.Contracts
{
    using BookInspector.DATA.Models;
    using System.Collections.Generic;

    public interface IBookService
    {
        Book GetById(int id);

        IEnumerable<Book> GetAll();
    }
}
