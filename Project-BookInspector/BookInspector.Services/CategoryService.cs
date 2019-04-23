
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using System.Collections.Generic;
    using BookInspector.Data.Context;
    using BookInspector.Services.Contracts;

    public class CategoryService : ICategoryService
    {
        private readonly BookInspectorContext _context;

        public CategoryService(BookInspectorContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Category AddCategory(string name)
        {
            var category = _context.Category.Where(x => x.Name.Equals(name)).SingleOrDefault();
            Validator.IfNotNull<ArgumentException>(category, $"Category {name} already exists");

            category = new Category() { Name = name };
            _context.Category.Add(category);
            _context.SaveChanges();
            return category;
        }

        public IReadOnlyCollection<Category> ShowAllCategories()
        {
            return _context.Category.ToList();
        }
    }
}

