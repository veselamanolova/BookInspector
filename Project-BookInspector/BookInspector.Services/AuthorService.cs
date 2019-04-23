
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.Data.Models;
    using BookInspector.Data.Context;
    using BookInspector.Services.Contracts;

    public class AuthorService : IAuthorService
    {
        private readonly BookInspectorContext _context;

        public AuthorService(BookInspectorContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Author Add(string name)
        {
            var author = _context.Author.Where(x => x.Name.Equals(name)).SingleOrDefault();
            Validator.IfNotNull<ArgumentException>(author, $"Author {name} already exists");

            author = new Author() { Name = name };

            _context.Author.Add(author);
            _context.SaveChanges();
            return author; 
        }

        public Dictionary<string, List<string>> Search(string args)
        {
            var authors = _context.Author.Where(a => a.Name.Contains(args))
                .Select(authorBook => new 
                {
                    Author = authorBook.Name,
                    Books = authorBook.BookByAuthor.Select(book =>

                        new string("\n" + book.Book.Title + " \nCategory: " +
                        string.Join(", ", book.Book.BookByCategory.Select(c => c.Category.Name)))
                        ).ToList()

                }).ToDictionary(key => key.Author, value => value.Books);

            return authors;
        }

        public IReadOnlyCollection<Author> GetAuthors()
        {
            return _context.Author.ToList(); 
        }
    }
}

