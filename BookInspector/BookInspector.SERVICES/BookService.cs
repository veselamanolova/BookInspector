
namespace BookInspector.SERVICES
{
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.DATA;
    using BookInspector.DATA.Models;
    using BookInspector.SERVICES.Contracts;
    using Microsoft.EntityFrameworkCore;
    using BookInspector.SERVICES.DTOs;
    using System;
    using System.Threading.Tasks;

    public class BookService : IBookService
    {
        private const int booksPerPage = 5;

        private readonly ApplicationDbContext _context;

        private IAuthorService _authorService;

        private IPublisherService _publisherService;

        private ICategoryService _categoryService;

        public BookService(ApplicationDbContext context,
            IAuthorService authorService,
            IPublisherService publisherService,
            ICategoryService categoryService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
            _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public Book GetById(int id)
        {
           return _context.Books.Where(book => book.Id.Equals(id))
                .Include(book => book.BooksCategories)
                    .ThenInclude(category => category.Category)
                .Include(book => book.BooksAuthors)
                    .ThenInclude(author => author.Author)
                .Include(book => book.FavoriteBooks)
                .Include(book => book.BooksRatings)
                .First();
        }

		public IEnumerable<Book> GetByCategory(string selectedCategory)
        {
            return _context.Books
                .Where(book => book.BooksCategories
                    .Select(category => category.Category.CategoryName).Contains(selectedCategory))
                .Include(bookCategory => bookCategory.BooksCategories)
                    .ThenInclude(category => category.Category);
        }
        public IEnumerable<Book> GetAll()
        {
            return _context.Books
                .Include(book => book.BooksCategories)
                    .ThenInclude(category => category.Category)
                .Include(book => book.BooksAuthors)
                    .ThenInclude(author => author.Author)
                .Include(book => book.FavoriteBooks)
                .Include(book => book.BooksRatings);
        }
		
		public int GetTotalBooksCount()
        {
            return _context.Books.Count();
        }

        public int GetBooksPerPage()
        {
            return booksPerPage;
        }

        public IEnumerable<Book> LoadNext()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> LoadPrevious()
        {
            throw new NotImplementedException();
		}
		
		
		public async Task DeleteBookAsync(int Id)	
        {
            Validator.IfNull<ArgumentNullException>(Id, "Book ID cannot be negative or 0.");
            var bookToDelete = await _context.Books
                .Where(book => book.Id == Id)
                .FirstOrDefaultAsync();

            Validator.IfNull<ArgumentNullException>(bookToDelete, "Book not found!");

   
            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync();
        }


        public IEnumerable<BookShortDTO> GetShortBooks()
        {
            var books = from b in _context.Books
                        select new BookShortDTO()
                        {
                            Id = b.Id,
                            Title = b.Title,
                            PublishedDate = b.PublishedDate,
                            ImageURL = b.ImageURL,
                            ShortDescription = b.ShortDescription,
                            PublisherName = b.Publisher.PublisherName,
                            AuthorNames = b.BooksAuthors.Select(x => x.Author.AuthorName).ToList()
                        };

            return books.ToList();

        }



        public BookDetailsDTO GetBookDetailsById(int id)
        {
            var book = from b in _context.Books
                       where b.Id == id
                       select new BookDetailsDTO()
                       {
                           Id = b.Id,
                           Title = b.Title,
                           PublishedDate = b.PublishedDate,
                           PublisherName = b.Publisher.PublisherName,
                           ImageURL = b.ImageURL,
                           Categories = b.BooksCategories.Select(x => x.Category.CategoryName).ToList(),
                           PreviewLink = b.PreviewLink,
                           Description = b.Description,                          
                           AuthorNames = b.BooksAuthors.Select(x => x.Author.AuthorName).ToList()
                       };             

            return book.FirstOrDefault(); 
        }
		
		

        public Book AddBook(string title,
           List<string> authorsList,
           List<string> categoryList,
           string publisherName,
           DateTime publishedDate,
           string isbn,
           string imabeUrl,
           string description, 
           string shortDescription,
           string previewLink)
        {

            var existingbook = _context.Books.FirstOrDefault(b => (b.Title == title&&b.Publisher.PublisherName== publisherName&&b.PublishedDate==publishedDate));
            var publisher = _context.Publishers.FirstOrDefault(p => p.PublisherName == publisherName);
            if(existingbook !=null)
            {
                throw new ArgumentException($"Book with title: {existingbook.Title}, publisher {publisherName} and date {publishedDate} already exists");
            }
         //The validation does not work correctly. Although existingbook.Title is null it enters in the validation and throws exception
         // Validator.IfNotNull<ArgumentException>(existingbook, $"Book with title: {existingbook.Title} already exists");
         //   Validator.CheckExactLength<ArgumentException>(isbn, 13, $"ISBN should be exactly 13 characters!");

            if (publisher is null)
            {
                publisher = _publisherService.Add(publisherName);
            }

            if (publishedDate.Year > DateTime.Now.Year || publishedDate.Year < (new DateTime(1500, 1, 1).Year))
            {
                throw new ArgumentException($"The date should be bigger than 1500 and smaller than next year.");
            }

            var book = new Book()
            {
                Title = title,
                PublisherId = publisher.Id,
                PublishedDate = publishedDate,
                Isbn = isbn,                
                Description = description, 
                ImageURL = imabeUrl,
                ShortDescription = shortDescription,
                PreviewLink = previewLink
            };

            _context.Books.Add(book);


            foreach (var authorName in authorsList)
            {
                var author = _context.Authors.FirstOrDefault(a => a.AuthorName == authorName);

                if (author == null)
                {
                    author = _authorService.Add(authorName);
                }

                var bookAuthorEntry = new BookAuthor()
                {
                    Author = author,
                    Book = book
                };
                _context.BooksAuthors.Add(bookAuthorEntry); 
            }

            foreach (var categoryName in categoryList)
            {
                var category = _context.Categories.FirstOrDefault(c => c.CategoryName == categoryName);

                if (category == null)
                {
                    category = _categoryService.AddCategory(categoryName);
                }

                var bookByAuthorEntry = new BookCategory()
                {
                    Category = category,
                    Book = book
                };
                _context.BooksCategories.Add(bookByAuthorEntry); 
            }

            _context.SaveChanges();
            return book;
        }



    }
}

