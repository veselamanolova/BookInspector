using System.Collections.Generic;
using BookInspector.DATA.Models;

namespace BookInspector.SERVICES.Contracts
{
    public interface IJsonBooksImporterService
    {
        List<Book> ImportBooks(string filePath, bool isFromWeb);
    }
}