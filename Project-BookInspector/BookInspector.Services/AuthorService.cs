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
            Validator.IfExist<ArgumentException>(name, $"Author {name} already exists");

            var author = new Author() { Name = name };

            _context.Author.Add(author);
            _context.SaveChanges();
            return author; 
        }

        public IReadOnlyCollection<Author> GetAuthors()
        {
            return _context.Author.ToList(); 
        }        
    }
}

