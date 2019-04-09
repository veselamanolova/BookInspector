
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
           
            var user  = _context.User.FirstOrDefault(u => u.Name == username);

            if (user==null)
            {
                throw new ArgumentException($"User {username} does not exists");
            }            

            var book = _context.Book.FirstOrDefault(b => b.Title == title);

            if (book==null)
            {
                throw new ArgumentException($"Book with title: {title} does not exists");
            }

            if (_context.RatingByBook.Any(u => u.BookId == book.BookId && u.UserId == user.UserId))
            {
                throw new ArgumentException($"User {username} already has rated the book {book.BookId}");
            }

            // Validator.IsInRange(rating, 0, 5); 
           

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

        public double GetAvarageRating(string title)
        {
            var book = _context.Book.FirstOrDefault(b => b.Title == title);

            if (book == null)
            {
                throw new ArgumentException($"Book with title: {title} does not exists");
            }

           var  averageRating = _context.RatingByBook.Where(r => r.BookId == book.BookId).Average(b =>b.Rating);
           _context.SaveChanges();

           return averageRating;
        }      
    }
}



