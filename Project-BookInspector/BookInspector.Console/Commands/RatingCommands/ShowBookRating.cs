
namespace BookInspector.Console.Commands
{
    using System;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class ShowBookRating : ICommand
    {
        private readonly IRatingService _ratingService;

        public ShowBookRating(IRatingService ratingService)
        {
            _ratingService = ratingService ?? throw new ArgumentNullException(nameof(ratingService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (args.Count < 1)
            {
                throw new ArgumentException("Please provide a book Title.");
            }

            string title = args[0];
            if (title == string.Empty)
            {
                throw new ArgumentException("Please provide a book Title.");
            }      

            double bookRating = _ratingService.GetAvarageRating(title);
            if (bookRating == 0)
            {
                return $"The book: {title} has no rating.";
            }
            else
            {
                return $"The book: {title} has rating: {bookRating:F2}.";
            }
            
        }
    }
}
