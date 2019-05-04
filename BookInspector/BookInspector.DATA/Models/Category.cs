
namespace BookInspector.DATA.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public virtual IEnumerable<BookCategory> BookCategory { get; set; }
    }
}
