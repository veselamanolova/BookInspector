
namespace BookInspector.SERVICES
{
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.DATA;
    using BookInspector.DATA.Models;
    using BookInspector.SERVICES.Contracts;
    using Microsoft.EntityFrameworkCore;
    using BookInspector.SERVICES.DTOs;

    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Book GetById(int id)
        {
            return _context.Books.Where(book => book.Id.Equals(id))
                //.Include(book => book.BookCategories)
                //.ThenInclude(bookCategories => bookCategories.Category.CategoryName)
                .Include(book => book.Publisher)
                //.Include(book => book.Authors)
                .First();
        }

        public IEnumerable<Book> GetAll()
        {
            var books = _context.Books
            .Include(book => book.BooksCategories)
            .ThenInclude(bookCategories => bookCategories.Category)
            .Include(book => book.Publisher)
             .Include(book => book.BooksAuthors); 
           //  .ThenInclude();

          //  var authors = _context.Autors

           

            var list = books.ToList(); 
            return books; 



        }


        public IEnumerable<BookShortDTO> GetShortBooks()
        {
            var books = from b in _context.Books
                        select new BookShortDTO()
                        {
                            Id = b.Id,
                            Title = b.Title,
                            PublishedDate = b.PublishedDate,
                            ImageURL = b.ImageURL,
                            ShortDescription = b.ShortDescription,
                            PublisherName = b.Publisher.PublisherName,
                            AuthorNames = b.BooksAuthors.Select(x => x.Author.AuthorName).ToList()
                        };           

            return books.ToList(); 

        }




    }
}

