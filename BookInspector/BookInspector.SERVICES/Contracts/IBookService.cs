
namespace BookInspector.SERVICES.Contracts
{
    using BookInspector.DATA.Models;
    using System.Collections.Generic;

    public interface IBookService
    {
        IEnumerable<Book> GetAll();
    }
}
