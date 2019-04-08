namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using System.Collections.Generic;
    using BookInspector.Data.Context;
    using BookInspector.Services.Contracts;

    public class RatingService : IRatingService
    {

        private readonly BookInspectorContext context;

        public RatingService(BookInspectorContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public RatingForBookByUser AddRating(string title, string username, int rating)
        {
           
            var user  = this.context.User.FirstOrDefault(u => u.Name == username);

            if (user==null)
            {
                throw new ArgumentException($"User {username} does not exists");
            }            

            var book = this.context.Book.FirstOrDefault(b => b.Title == title);

            if (book==null)
            {
                throw new ArgumentException($"Book with title: {title} does not exists");
            }

            if (this.context.RatingForBookByUser.Any(u => u.BookId == book.BookId && u.UserId == user.UserId))
            {
                throw new ArgumentException($"User {username} already has rated the book {book.BookId}");
            }

            if (rating > 5 || rating <= 0)
            {
                throw new ArgumentException($"Rating should be a value between 1 and 5!");
            }

            var ratingForBookByUser = new RatingForBookByUser()
            {
                BookId = book.BookId, 
                UserId = user.UserId, 
                Rating = rating
            };
            this.context.RatingForBookByUser.Add(ratingForBookByUser);
            this.context.SaveChanges();

            return ratingForBookByUser;
        }

        
    }
}



