
namespace BookInspector.App.Commands.CategoryCommands
{
    using System;
    using System.Linq;
    using BookInspector.Services;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class ShowAllCategories : ICommand
    {
        private readonly ICategoryService _categoryService;

        public ShowAllCategories(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(CategoryService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            return string.Join(", ", _categoryService.ShowAllCategories().Select(x => x.Name));
        }
    }
}