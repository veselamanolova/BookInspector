namespace BookInspector.Services
{
    using BookInspector.Data.Context;
    using BookInspector.Data.Models;
    using BookInspector.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AuthorService : IAuthorService
    {
        private readonly BookInspectorContext context;

        public AuthorService(BookInspectorContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Author Add(string name)
        {
            if(this.context.Author.Any(a=> a.Name == name))
            {
                throw new ArgumentException($"Author {name} already exists");
            }

            var author = new Author()
            {
                Name = name
            };

            this.context.Author.Add(author);
            this.context.SaveChanges();
            return author; 
        }

        public Author FindByName(string name)
        {
            return this.context.Author
                 .FirstOrDefault(a => a.Name == name); 
        }

        public IReadOnlyCollection<Author> GetAuthors(int skip, int take)
        {
            return this.context.Author
                .Skip(skip)
                .Take(take)
                .ToList(); 
        }        
    }
}

