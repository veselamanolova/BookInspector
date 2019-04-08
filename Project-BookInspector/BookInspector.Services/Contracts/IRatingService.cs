using BookInspector.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookInspector.Services.Contracts
{
    interface IRatingService
    {
        RatingForBookByUser AddRating(string title, string username, int rating);

      //  IReadOnlyCollection<RatingForBookByUser> GetBookRating(int BookID);

    }
}
