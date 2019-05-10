
namespace BookInspector.DATA.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser
    {
        public virtual IEnumerable<FavoriteBook> FavoriteBook { get; set; }

        public virtual IEnumerable<UserBookRating> RatingByBook { get; set; }
    }
}
