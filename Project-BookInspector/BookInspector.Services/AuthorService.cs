﻿
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
            if (_context.Author.Any(a => a.Name == name))
                throw new ArgumentException($"Author {name} already exists");

            var author = new Author() { Name = name };

            _context.Author.Add(author);
            _context.SaveChanges();
            return author; 
        }

        public Author FindByName(string name)
        {
            return _context.Author.FirstOrDefault(a => a.Name == name); 
        }

        public IReadOnlyCollection<Author> GetAuthors(int skip, int take)
        {
            return _context.Author
                .Skip(skip)
                .Take(take)
                .ToList(); 
        }        
    }
}

