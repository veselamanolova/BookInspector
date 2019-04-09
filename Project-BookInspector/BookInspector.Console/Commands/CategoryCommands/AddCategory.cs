
namespace BookInspector.App.Commands
{
    using System;
    using System.Linq;
    using BookInspector.Services;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class AddCategory : ICommand
    {
        private readonly ICategoryService _categoryService;

        public AddCategory(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(CategoryService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (!args.Any()) throw new ArgumentException("Please provide a category name as first parameter");
            
            var category = _categoryService.AddCategory(args[0]);

            return $"Category {category.Name}, Id = {category.Name} added";
        }
    }
}