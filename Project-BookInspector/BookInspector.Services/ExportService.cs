
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using SautinSoft.Document;
    using BookInspector.Data.Context;
    using BookInspector.Services.Contracts;

    public sealed class ExportService : IExportService
    {
        private readonly BookInspectorContext _context;

        public ExportService(BookInspectorContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public void ListBooksToPDF()
        {
            var books = _context.Book
                .Select(book => new
                {
                    ID = book.BookId + "\n",
                    Name = book.Title + "\n",
                    Category = string.Join(" ", book.BookByCategory.Select(bookCategory => bookCategory.Category.Name).ToList()) + "\n",
                    Author = string.Join(" ", book.BookByAuthor.Select(bookAuthor => bookAuthor.Author.Name).ToList()) + "\n",
                    Publisher = book.Publisher.Name + "\n",
                    PublishedDate = book.PublishedDate + "\n"
                })
                .OrderByDescending(book => book.ID)
                .ToList();

            DocumentCore documentCore = new DocumentCore();

            documentCore.Content.End
                .Insert("Books List: " + '\n', new CharacterFormat() { FontName = "Verdana", Size = 35.5f, FontColor = Color.Orange });
            foreach (var book in books)
            {
                documentCore
                    .Content
                    .End
                    .Insert("\n" + new string('-', 40) + '\n', new CharacterFormat() { FontName = "Verdana", Size = 7.5f, FontColor = Color.Orange });
                documentCore.Content.End.Insert(book.ToString(), new CharacterFormat() { FontName = "Verdana", Size = 7.5f });
            }

            string filePath = @"BookList.pdf";
            documentCore.Save(filePath);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
        }
    }
}

