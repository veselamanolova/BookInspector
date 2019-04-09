
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using BookInspector.Data.Context;
    using System.Collections.Generic;
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
            Validator.IfExist<ArgumentException>(
                _context.Publisher.Select(x => x.Name).ToList(), name, $"Publisher {name} already exists");

            var publisher = new Publisher() { Name = name };
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
