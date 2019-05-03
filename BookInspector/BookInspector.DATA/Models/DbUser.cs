using System;
using System.Collections.Generic;
using System.Text;

namespace BookInspector.DATA.Models
{
    public class DbUser: ApplicationUser
    {
        public virtual IEnumerable<FavoriteBook> FavoriteBook { get; set; }

        public virtual IEnumerable<UserBookRating> RatingByBook { get; set; }
    }
}
