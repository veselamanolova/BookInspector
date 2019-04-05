namespace BookInspector.Console.Commands.AuthorCommands
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.Console.Contracts;
    using BookInspector.Services.Interfaces;
    using BookInspector.Services;

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
                throw new ArgumentException("Please provide a author name as first parameter");
            }

            var author = this.authorService.Add(args[0]);

            return $"Author {author.Name}, Id = {author.Name} added";
        }

    }
}

