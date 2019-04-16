
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.Data.Models;
    using BookInspector.Data.Repository;
    using BookInspector.Services.Contracts;

    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorsRepository;

        public AuthorService(IRepository<Author> authorsRepository)
        {
            _authorsRepository = authorsRepository ?? throw new ArgumentNullException(nameof(authorsRepository));
        }

        public Author Add(string name)
        {
            var author = _authorsRepository.All().Where(x => x.Name.Equals(name)).SingleOrDefault();
            Validator.IfNotNull<ArgumentException>(author, $"Author {name} already exists");

            author = new Author() { Name = name };

            _authorsRepository.Add(author);
            _authorsRepository.Save();
            return author; 
        }

        public Dictionary<string, List<string>> Search(string args)
        {
            var authors = _authorsRepository.All().Where(a => a.Name.Contains(args))
                .Select(authorBook => new 
                {
                    Author = authorBook.Name,
                    Books = authorBook.BookByAuthor.Select(book => new
                    {
                        bookCategory = "\n" + book.Book.Title + " \nCategory: " + 
                                       string.Join(", ", book.Book.BookByCategory.Select(x => x.Category.Name))
                    }).ToList()
                }).ToDictionary(key => key.Author, value => value.Books.Select(x => x.bookCategory).ToList());

            return authors;
        }

        public IReadOnlyCollection<Author> GetAuthors()
        {
            return _authorsRepository.All().ToList(); 
        }
    }
}

