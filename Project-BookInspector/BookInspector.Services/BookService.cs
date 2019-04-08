
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
        private readonly IPublisherService _publisherService;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;

        public BookService(BookInspectorContext context, IPublisherService publisherService, 
            IAuthorService authorService, ICategoryService categoryService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
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

            if (existingbook != null)
            {
                throw new ArgumentException($"Book with title: {existingbook.Title} already exists");
            }

           
            var publisher = _context.Publisher.FirstOrDefault(p => p.Name == publisherName);
            if (publisher == null)
            {
                publisher = _publisherService.Add(publisherName);                 
            }

            if(publishedDate.Year>DateTime.Now.Year|| publishedDate.Year< (new DateTime(1500, 1, 1).Year))
            {
                throw new ArgumentException($"The date should be bigger than 1500 and smaller than next year.");
            }

            Validation.CheckExactLength(isbn, 13, "isbn");

            Validation.IsInRange(volumeId, 1, 100, "volume Id");

            Validation.IsInRange(pageCount, 1, 5000, "pageCount");

                        
            

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
                
                if (author == null)
                {
                    author = _authorService.Add(authorName); 
                }
               
                var bookByAuthorEntry = new BookByAuthor()
                {
                    Author = author,
                    Book = book
                };
                _context.BookByAuthor.Add(bookByAuthorEntry);                    
                
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
                _context.BookByCategory.Add(bookByCategoryEntry);                   
               
            }

            _context.SaveChanges();
            return book;
        } 
    }
}
