
namespace BookInspector.DATA.Models
{
    using System.Collections.Generic;

    public class Publisher
    {
        public int Id { get; set; }

        public string PublisherName { get; set; }

        public virtual IEnumerable<Book> Books { get; set; }
    }
}
