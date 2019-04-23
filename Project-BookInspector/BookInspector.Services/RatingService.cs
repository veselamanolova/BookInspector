
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using BookInspector.Data.Context;
    
    using BookInspector.Services.Contracts;

    public class RatingService : IRatingService
    {
        private readonly BookInspectorContext _context;

        public RatingService(BookInspectorContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public RatingForBookByUser AddRating(string title, string username, int rating)
        {
            var book = _context.Book.FirstOrDefault(b => b.Title == title);
            var user = _context.User.FirstOrDefault(u => u.Name == username);
           

            Validator.IfNull<ArgumentException>(book, "The book does not exist!" );
            Validator.IfNull<ArgumentException>(user, "The user does not exist!");
            Validator.IsInRange<ArgumentException>(rating, 1, 5, "Invalid rating. Rating should be between 1 and 5.");

            if (_context.RatingByBook.Any(u => u.BookId == book.BookId && u.UserId == user.UserId))
                throw new ArgumentException($"User {username} already has rated the book {book.Title}");

            var ratingForBookByUser = new RatingForBookByUser()
            {
                BookId = book.BookId,
                UserId = user.UserId,
                Rating = rating
            };

            _context.RatingByBook.Add(ratingForBookByUser);
            _context.SaveChanges();

            return ratingForBookByUser;
        }

        public double GetAverageRating(string title)
        {
            var book = _context.Book.FirstOrDefault(b => b.Title == title);
            Validator.IfNull<ArgumentException>(book, "The book does not exist!");

            var ratingQuery = _context.RatingByBook.Where(r => r.BookId == book.BookId);

            if (ratingQuery.LongCount() is 0)
                return 0;

            return ratingQuery.Average(b => b.Rating);
        }
    }
}


