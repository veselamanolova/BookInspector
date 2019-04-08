
namespace BookInspector.Console.Commands
{
    using System;
    using System.Linq;
    using BookInspector.Services;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class AddAuthor : ICommand
    {
        private readonly IAuthorService _authorService;

        public AddAuthor(IAuthorService authorService)
        {
            _authorService = authorService ?? throw new ArgumentNullException(nameof(AuthorService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (!args.Any())
            {
                throw new ArgumentException("Please provide a author name as first parameter");
            }

            var author = _authorService.Add(args[0]);

            return $"Author {author.Name}, Id = {author.Name} added";
        }

    }
}

