
namespace BookInspector.Console.Commands
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.Services;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class AddAuthor : ICommand
    {
        private readonly IAuthorService authorService;

        public AddAuthor(IAuthorService authorService)
        {
            this.authorService = authorService ?? throw new ArgumentNullException(nameof(AuthorService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (!args.Any())
            {
                throw new ArgumentException("Please provide a author name");
            }

            var author = this.authorService.Add(string.Join(" ", args));

            return $"Author {author.Name}, Id = {author.Name} added";
        }

    }
}

