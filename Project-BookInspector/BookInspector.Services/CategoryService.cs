
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using BookInspector.Data.Context;
    using System.Collections.Generic;
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
            if (_context.Category.Any(c => c.Name.Equals(name)))
                throw new ArgumentException($"Category {name} already exists");

            var category = new Category() { Name = name };

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