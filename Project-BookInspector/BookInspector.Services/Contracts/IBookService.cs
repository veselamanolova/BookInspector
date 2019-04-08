namespace BookInspector.Services.Contracts
{
    using System;
    using BookInspector.Data.Models;    
    using System.Collections.Generic;

    public interface IBookService
    {
        Book AddBook(string title, List<string> authorsList, List<string> categoryList, string publisher, DateTime publishedDate, string isbn, int volumeId, int pageCount, string description);
    }
}

