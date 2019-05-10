
namespace BookInspector.SERVICES
{
    using System.Linq;
    using SautinSoft.Document;
    using BookInspector.SERVICES.Contracts;

    public class ExportService : IExportService
    {
        private IBookService _bookService;

        public ExportService(IBookService bookService)
        {
            _bookService = bookService;
        }

        public void ExportToPDF()
        {
            var books = _bookService.GetAll()
                .Select(book => new
                {
                    ID = book.Id + "\n",
                    Name = book.Title + "\n"
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


