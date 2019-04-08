
namespace BookInspector.Console.Commands
{
    using System;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class AddRating : ICommand
    {
        private readonly IRatingService _ratingService;

        public AddRating(IRatingService ratingService)
        {
            _ratingService = ratingService ?? throw new ArgumentNullException(nameof(ratingService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (args.Count<3)
            {
                throw new ArgumentException("Please provide a Title as first parameter, User name as a second parameter and a rating as a third parameter.");
            }

            string title = args[0];
            if (title == string.Empty)
            {
                throw new ArgumentException("Please provide a Title as first parameter.");
            }
            string username = args[1]; 

            if (username == string.Empty)
            {
                throw new ArgumentException("Please provide a User name as a second parameter.");
            }

           // int rating;

            bool success = Int32.TryParse(args[2], out int rating);

            //check if it is correct
            if (!success)
            {
                throw new ArgumentException("Please provide a rating from 1 to 5 as a third parameter");
            }           

            var ratingForBookByUser = _ratingService.AddRating(title, username, rating);

            return $"User {username} added rating {rating} to book {title}";
        }

    }
}

