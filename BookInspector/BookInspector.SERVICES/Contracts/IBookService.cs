
namespace BookInspector.SERVICES.Contracts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
	using System.Collections.Generic;
    using BookInspector.DATA.Models;    
    using BookInspector.SERVICES.DTOs;

    public interface IBookService
    {
        IQueryable<Book> GetAll();

        Task<Book> GetByIdAsync(int id);

        Task<IEnumerable<Book>> GetByCategoryAsync(string selectedCategory);

        Task<IEnumerable<Book>> SearchAsync(string key);

        Task DeleteBookAsync(int Id);

        BookDetailsDTO GetBookDetailsById(int id);

        Book AddBook(string title,
             List<string> authorsList,
             List<string> categoryList,
             string publisherName,
             DateTime publishedDate,
             string isbn,
             string imageUrl,
             string description,
             string shortDescription,
             string previewLink);

        IEnumerable<BookShortDTO> GetShortBooks();
    }
	
	
}
