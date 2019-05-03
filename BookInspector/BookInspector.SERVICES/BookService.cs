
namespace BookInspector.SERVICES
{
    using System.Collections.Generic;
    using BookInspector.DATA;
    using BookInspector.DATA.Models;
    using BookInspector.SERVICES.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books
                .Include(book => book.Category)
                .Include(book => book.Publisher)
                .Include(book => book.Authors);
        }
    }
}
