
namespace BookInspector.Data.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<BookByCategory> BookByCategory { get; set; }
    }
}