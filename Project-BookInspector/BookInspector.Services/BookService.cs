
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

        public BookService(BookInspectorContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Book AddBook(string title, 
            List<string> authorsList, 
            List<string> categoryList, 
            string publisher, 
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

           
            var _publisher = _context.Publisher.FirstOrDefault(p => p.Name == publisher);
            if (_publisher == null)
            {
                throw new ArgumentException($"The publisher: {publisher} does not exist. Please add the publisher first.");
            }

            if(publishedDate.Year>DateTime.Now.Year|| publishedDate.Year< (new DateTime(1900, 1, 1).Year))
            {
                throw new ArgumentException($"The date should be bigger than 1899 and smaller than next year.");
            }

            Validation.CheckExactLength(isbn, 13, "isbn");

            Validation.IsInRange(volumeId, 1, 100, "volume Id");

            Validation.IsInRange(pageCount, 1, 5000, "pageCount");

                        
            

            var book = new Book()
            {               
                Title = title,
                PublisherId = _publisher.PublisherId,
                PublishedDate= publishedDate,
                Isbn = isbn,
                VolumeId= volumeId,
                PageCount= pageCount,
                Description= description
            };
            _context.Book.Add(book);
            _context.SaveChanges();

            foreach (var authorName in authorsList)
            {
                var author = _context.Author.FirstOrDefault(a => a.Name == authorName);
                
                if (author == null)
                {
                    throw new ArgumentException($"The author: {authorName} does not exist. Please add the author first.");
                }
                else
                {
                    var bookByAuthorEntry = new BookByAuthor()
                    {
                        AuthorId = author.AuthorId,
                        BookId = book.BookId
                    };
                    _context.BookByAuthor.Add(bookByAuthorEntry);
                    _context.SaveChanges();
                }
            }

            foreach (var categoryName in categoryList)
            {
                var category = _context.Category.FirstOrDefault(c => c.Name == categoryName);

                if (category == null)
                {
                    throw new ArgumentException($"The category: {categoryName} does not exist. Please add the category first.");
                }
                else
                {
                    var bookByCategoryEntry = new BookByCategory()
                    {
                        CategoryId = category.CategoryId,
                        BookId = book.BookId
                    };
                    _context.BookByCategory.Add(bookByCategoryEntry);
                    _context.SaveChanges();
                }
            }
            
            return book;
        } 
    }
}
