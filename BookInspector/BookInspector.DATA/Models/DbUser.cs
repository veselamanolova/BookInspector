
namespace BookInspector.DATA.Models
{
    using System.Collections.Generic;

    public class DbUser: ApplicationUser
    {
        public virtual IEnumerable<FavoriteBook> FavoriteBook { get; set; }

        public virtual IEnumerable<UserBookRating> RatingByBook { get; set; }
    }
}
