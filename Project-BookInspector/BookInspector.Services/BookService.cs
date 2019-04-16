
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using System.Collections.Generic;
    using BookInspector.Data.Repository;
    using BookInspector.Services.Contracts;

    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Publisher> _publisherRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Category> _categoryRepository;

        private IAuthorService _authorService;
        private IPublisherService _publisherService;
        private ICategoryService _categoryService;

        public BookService(
            IRepository<Book> bookRepository,
            IRepository<Publisher> publisherRepository, 
            IRepository<Author> authorRepository,
            IRepository<Category> categoryRepository,
            IAuthorService authorService,
            IPublisherService publisherService,
            ICategoryService categoryService)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
            _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public Book AddBook(string title, 
            List<string> authorsList, 
            List<string> categoryList, 
            string publisherName, 
            DateTime publishedDate, 
            string isbn, 
            int volumeId, 
            int pageCount, 
            string description)
        {

            var existingbook = _bookRepository.All().FirstOrDefault(b => b.Title == title);
            var publisher = _publisherRepository.All().FirstOrDefault(p => p.Name == publisherName);
            

            Validator.IfNotNull<ArgumentException>(existingbook, $"Book with title: {existingbook.Title} already exists");
            Validator.IsInRange<ArgumentException>(volumeId, 1, 100, $"Allowed values for Volume Id are between 1 and 100");
            Validator.IsInRange<ArgumentException>(pageCount, 1, 5000, $"Allowed values for pageCount are between 1 and 5000");
            Validator.CheckExactLength<ArgumentException>(isbn, 13, $"ISBN should be exactly 13 characters!");

            if (publisher is null)
            {
                publisher = _publisherService.Add(publisherName);
            }             

            if (publishedDate.Year > DateTime.Now.Year|| publishedDate.Year < (new DateTime(1500, 1, 1).Year))
            {
                throw new ArgumentException($"The date should be bigger than 1500 and smaller than next year.");
            }
            
            var book = new Book()
            {               
                Title = title,
                PublisherId = publisher.PublisherId,
                PublishedDate= publishedDate,
                Isbn = isbn,
                VolumeId= volumeId,
                PageCount= pageCount,
                Description= description
            };

            _bookRepository.Add(book);
            

            foreach (var authorName in authorsList)
            {
                var author = _authorRepository.All().FirstOrDefault(a => a.Name == authorName);
                
                if (author is null)
                {
                    author = _authorService.Add(authorName); 
                }
               
                var bookByAuthorEntry = new BookByAuthor()
                {
                    Author = author,
                    Book = book
                };
                // _context.BookByAuthor.Add(bookByAuthorEntry);                    
            }

            foreach (var categoryName in categoryList)
            {
                var category = _categoryRepository.All().FirstOrDefault(c => c.Name == categoryName);

                if (category == null)
                {
                    category = _categoryService.AddCategory(categoryName);
                }
               
                var bookByCategoryEntry = new BookByCategory()
                {
                    Category = category,
                    Book = book
                };
                // _context.BookByCategory.Add(bookByCategoryEntry);                   
            }

            _bookRepository.Save();
            _authorRepository.Save();
            _publisherRepository.Save();
            return book;
        }

        
        public Dictionary<string, List<string>> Search(string args)
        {
            var books = _bookRepository.All().Where(x => x.Title.Contains(args)).Select(x => new
            {
                Name = x.Title,
                Authors = x.BookByAuthor.Select(b => b.Author.Name).ToList()
            }).ToDictionary(key => key.Name, value => value.Authors);

            return books;
        }
    }
}

