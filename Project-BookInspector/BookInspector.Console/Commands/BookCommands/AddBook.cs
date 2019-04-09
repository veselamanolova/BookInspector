namespace BookInspector.Console.Commands
{
    using System;
    using System.Linq;
    using BookInspector.Services;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class AddBook : ICommand
    {
        private readonly IBookService _bookService;

        public AddBook(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(BookService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (args.Count<9)
            {
                throw new ArgumentException("Not all book arguments are provided.");
            }

            string title = args[0];
            List<string> authorsList = args[1].Split('^').ToList();
            List<string> categoryList = args[2].Split('^').ToList();
            string publisher = args[3];
            if (!int.TryParse(args[4], out int publishedYear))
            {
                // Invalid published year
            }
            DateTime publishedDate = new DateTime(publishedYear, 1, 1);
            string isbn = args[5]; 
            if(!int.TryParse(args[6], out int volumeId))
            {
                //throw
            }
            if (!int.TryParse(args[7], out int pageCount))
            {
                //throw
            }
            string description = args[8]; 
        

            var book = _bookService.AddBook(title,
                authorsList,
                categoryList,
                publisher,
                publishedDate,
                isbn,
                volumeId,
                pageCount,
                description);

            return $"Book {book.Title}, Id = {book.BookId} added";
        }
    }
}