namespace BookInspector.DATA
{
    using System.Collections.Generic;

    public class Publisher
    {
        public Publisher()
        {
            BookByPublisher = new HashSet<Book>();
        }

        public int PublisherId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> BookByPublisher { get; set; }
    }
}