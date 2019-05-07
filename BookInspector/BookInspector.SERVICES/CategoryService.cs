
namespace BookInspector.SERVICES
{
    using System;
    using System.Linq;
    using BookInspector.DATA.Models;
    using System.Collections.Generic;
    using BookInspector.SERVICES.Contracts;
    using BookInspector.DATA;
    using BookInspector.SERVICES;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Category AddCategory(string name)
        {
            var category = _context.Categories.Where(x => x.CategoryName.Equals(name)).SingleOrDefault();
            Validator.IfNotNull<ArgumentException>(category, $"Category {name} already exists");

            category = new Category() { CategoryName = name };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public IReadOnlyCollection<Category> ShowAllCategories()
        {
            return _context.Categories.ToList();
        }
    }
}

