using System.Collections.Generic;
using BookInspector.Data.Models;

namespace BookInspector.Services.Contracts
{
    public interface IJsonBooksImporterService
    {
        List<Book> ImportBooks(string filePath, bool isFromWeb);
    }
}