
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using BookInspector.Data.Repository;
    using BookInspector.Services.Contracts;

    public class RatingService : IRatingService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RatingForBookByUser> _ratingForBookByUser;

        public RatingService(IRepository<Book> bookRepository, IRepository<User> userRepository, IRepository<RatingForBookByUser> ratingForBookByUser)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _ratingForBookByUser = ratingForBookByUser ?? throw new ArgumentNullException(nameof(ratingForBookByUser));
        }

        public RatingForBookByUser AddRating(string title, string username, int rating)
        {
            var book = _bookRepository.All().FirstOrDefault(b => b.Title == title);
            var user = _userRepository.All().FirstOrDefault(u => u.Name == username);
           

            Validator.IfNull<ArgumentException>(book, "The book does not exist!" );
            Validator.IfNull<ArgumentException>(user, "The user does not exist!");
            Validator.IsInRange<ArgumentException>(rating, 1, 5, "Invalid rating. Rating should be between 1 and 5.");

            if (_ratingForBookByUser.All().Any(u => u.BookId == book.BookId && u.UserId == user.UserId))
                throw new ArgumentException($"User {username} already has rated the book {book.Title}");

            var ratingForBookByUser = new RatingForBookByUser()
            {
                BookId = book.BookId,
                UserId = user.UserId,
                Rating = rating
            };

            _ratingForBookByUser.Add(ratingForBookByUser);
            _ratingForBookByUser.Save();

            return ratingForBookByUser;
        }

        public double GetAverageRating(string title)
        {
            var book = _bookRepository.All().FirstOrDefault(b => b.Title == title);
            Validator.IfNull<ArgumentException>(book, "The book does not exist!");

            var ratingQuery = _ratingForBookByUser.All().Where(r => r.BookId == book.BookId);

            if (ratingQuery.LongCount() is 0)
                return 0;

            return ratingQuery.Average(b => b.Rating);
        }
    }
}


