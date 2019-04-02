namespace BookInspector.DATA
{
    using System.Collections.Generic;

    public class Category
    {
        public Category()
        {
            BookByCategory = new HashSet<Book>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> BookByCategory { get; set; }
    }
}