
using System.Linq;

namespace BookInspector.App.Commands.AuthorCommands
{
    using System;
    using System.Text;
    using BookInspector.Services;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;
    
    public class SearchAuthor : ICommand
    {
        private readonly IAuthorService _authorService;

        public SearchAuthor(IAuthorService authorService)
        {
            _authorService = authorService ?? throw new ArgumentNullException(nameof(AuthorService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            StringBuilder bookBuilder = new StringBuilder();

            var authorCollection = _authorService.Search(args[0]);

            foreach (var author in authorCollection)
            {
                if (!author.Value.Any()) continue;

                bookBuilder.AppendLine($"{author.Key}: {string.Join(", ", author.Value)}");
            }

            return bookBuilder.ToString();
        }
    }
}

