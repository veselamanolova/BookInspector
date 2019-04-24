
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using System.Collections.Generic;
    using BookInspector.Data.Context;
    using BookInspector.Services.Contracts;

    public class BookService : IBookService
    {
        private readonly BookInspectorContext _context;

        private IAuthorService _authorService;
        private IPublisherService _publisherService;
        private ICategoryService _categoryService;

        public BookService(
            BookInspectorContext context,
            IAuthorService authorService,
            IPublisherService publisherService,
            ICategoryService categoryService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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

            var existingbook = _context.Book.FirstOrDefault(b => b.Title == title);
            var publisher = _context.Publisher.FirstOrDefault(p => p.Name == publisherName);
            

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

            _context.Book.Add(book);
            

            foreach (var authorName in authorsList)
            {
                var author = _context.Author.FirstOrDefault(a => a.Name == authorName);
                
                if (author is null)
                {
                    author = _authorService.Add(authorName); 
                }
               
                var bookByAuthorEntry = new BookByAuthor()
                {
                    Author = author,
                    Book = book
                };                   
            }

            foreach (var categoryName in categoryList)
            {
                var category = _context.Category.FirstOrDefault(c => c.Name == categoryName);

                if (category == null)
                {
                    category = _categoryService.AddCategory(categoryName);
                }
               
                var bookByCategoryEntry = new BookByCategory()
                {
                    Category = category,
                    Book = book
                };                  
            }

            _context.SaveChanges();
            return book;
        }


        public IReadOnlyCollection<Book> Search(string args)
        {
            var list = _context.Book
                   .Where(x => x.Title.Contains(args))
                   .ToList();

            return list;
        }


        /*
public Dictionary<string, List<string>> Search(string args)
{
   var books = _context.Book.Where(x => x.Title.Contains(args)).Select(x => new
   {
       Name = x.Title,
       Authors = x.BookByAuthor.Select(b => b.Author.Name).ToList()
   }).ToDictionary(key => key.Name, value => value.Authors);

   return books;
}
*/
    }
}

