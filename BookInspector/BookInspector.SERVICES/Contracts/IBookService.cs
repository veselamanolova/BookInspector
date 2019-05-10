
namespace BookInspector.SERVICES.Contracts
{
	using System.Threading.Tasks;
	using System.Collections.Generic;
    using BookInspector.DATA.Models;    
    using BookInspector.SERVICES.DTOs;
    using System;
    using System.Collections.Generic;

    public interface IBookService
    {
        Book GetById(int id);
        
        IEnumerable<Book> GetByCategory(string selectedCategory);

        IEnumerable<Book> GetAll();

        int GetBooksPerPage();

        int GetTotalBooksCount();

        IEnumerable<Book> LoadNext();

        IEnumerable<Book> LoadPrevious();

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
