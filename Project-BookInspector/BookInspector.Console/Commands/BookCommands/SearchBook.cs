
namespace BookInspector.Console.Commands
{
    using System;
    using System.Text;
    using BookInspector.Services;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class SearchBook : ICommand
    {
        private readonly IBookService _bookService;

        public SearchBook(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(BookService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            StringBuilder bookBuilder = new StringBuilder();

            var bookCollection = _bookService.Search(args[0]);

            foreach (var book in bookCollection)
            {
                bookBuilder.AppendLine($"{book.Key}: {string.Join(", ", book.Value)}");
            }

            return bookBuilder.ToString();
        }
    }
}