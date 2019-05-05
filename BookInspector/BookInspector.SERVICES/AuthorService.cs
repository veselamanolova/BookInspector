
namespace BookInspector.SERVICES
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.DATA.Models;    
    using BookInspector.SERVICES.Contracts;
    using BookInspector.DATA;

    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Author Add(string name)
        {
            var author = _context.Authors.Where(x => x.AuthorName.Equals(name)).SingleOrDefault();
            Validator.IfNotNull<ArgumentException>(author, $"Author {name} already exists");

            author = new Author() { AuthorName = name };

            _context.Authors.Add(author);
            _context.SaveChanges();
            return author; 
        }

        public Dictionary<string, List<string>> Search(string args)
        {
            var authors = _context.Authors.Where(a => a.AuthorName.Contains(args))
                .Select(authorBook => new 
                {
                    Author = authorBook.AuthorName,
                    Books = authorBook.BooksAuthors.Select(book =>

                        new string("\n" + book.Book.Title + " \nCategory: " +
                        string.Join(", ", book.Book.BooksCategories.Select(c => c.Category.CategoryName)))
                        ).ToList()

                }).ToDictionary(key => key.Author, value => value.Books);

            return authors;
        }

        public IReadOnlyCollection<Author> GetAuthors()
        {
            return _context.Authors.ToList(); 
        }
    }
}

