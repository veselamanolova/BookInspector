
namespace BookInspector.DATA
{
    using System.Collections.Generic;

    public class Publisher
    {
        public int PublisherId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Book> BookByPublisher { get; set; }
    }
}