
namespace BookInspector.DATA
{
    using System.Collections.Generic;

    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Book> BookByCategory { get; set; }
    }
}