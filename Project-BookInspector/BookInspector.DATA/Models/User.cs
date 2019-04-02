
namespace BookInspector.DATA
{
    using System.Collections.Generic;

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FavoriteBook> FavoriteBook { get; set; }
    }
}

