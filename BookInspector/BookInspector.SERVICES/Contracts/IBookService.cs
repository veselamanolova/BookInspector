
namespace BookInspector.SERVICES.Contracts
{
    using BookInspector.DATA.Models;
    using BookInspector.SERVICES.DTOs;
    using System;
    using System.Collections.Generic;

    public interface IBookService
    {
        Book GetById(int id);

        IEnumerable<Book> GetAll();

        IEnumerable<BookShortDTO> GetShortBooks();

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
    }
}
