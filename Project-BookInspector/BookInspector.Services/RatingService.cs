﻿
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

            if (_context.RatingByBook.Any(u => u.BookId == book.BookId && u.UserId == user.UserId))
                throw new ArgumentException($"User {username} already has rated the book {book.BookId}");

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

            Validator.IfNull<ArgumentException>(book);

            var averageRating = _context
                .RatingByBook.Where(r => r.BookId == book.BookId)
                .Average(b => b.Rating);

            return averageRating;
        }
    }
}


