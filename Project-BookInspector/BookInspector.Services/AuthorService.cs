
using Microsoft.EntityFrameworkCore;

namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using System.Collections.Generic;
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
            Validator.IfExist<ArgumentException>(
                _context.Author.Select(x => x.Name).ToList(), name, $"Author {name} already exists");

            var author = new Author() { Name = name };

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
                    Books = authorBook.BookByAuthor.Select(book => book.Book.Title).ToList()
                }).ToDictionary(key => key.Author, value => value.Books);

            return authors;
        }

        public IReadOnlyCollection<Author> GetAuthors()
        {
            return _context.Author.ToList(); 
        }
    }
}

