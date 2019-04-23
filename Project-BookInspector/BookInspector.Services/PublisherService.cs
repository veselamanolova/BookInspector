
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.Data.Models;
    using BookInspector.Data.Context;
    using BookInspector.Services.Contracts;

    public class PublisherService : IPublisherService
    {
        private readonly BookInspectorContext _context;

        public PublisherService(BookInspectorContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Publisher Add(string name)
        {
            var publisher = _context.Publisher.Where(x => x.Name.Equals(name)).SingleOrDefault();
            Validator.IfNotNull<ArgumentException>(publisher, $"Publisher {name} already exists");

            publisher = new Publisher() { Name = name };
            _context.Publisher.Add(publisher);
            _context.SaveChanges();

            return publisher;
        }

        public IReadOnlyCollection<Publisher> GetPublishers()
        {
            return _context.Publisher.ToList();
        }       
    }
}
