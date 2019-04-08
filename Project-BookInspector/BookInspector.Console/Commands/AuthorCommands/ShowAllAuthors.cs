
namespace BookInspector.App.Commands
{
    using System;
    using System.Linq;
    using BookInspector.Services;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class ShowAllAuthors : ICommand
    {
        private readonly IAuthorService _authorService;

        public ShowAllAuthors(IAuthorService authorService)
        {
            _authorService = authorService ?? throw new ArgumentNullException(nameof(CategoryService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            return string.Join(", ", _authorService.GetAuthors().Select(x => x.Name));
        }
    }
}
