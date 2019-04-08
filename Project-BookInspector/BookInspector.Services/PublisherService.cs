
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
            if (_context.Publisher.Any(u => u.Name == name))
            {
                throw new ArgumentException($"Publisher {name} already exists");
            }

            var publisher = new Publisher() { Name = name };
            _context.Publisher.Add(publisher);
            _context.SaveChanges();

            return publisher;
        }

        public Publisher FindByName(string name)
        {
            return _context.Publisher
                .FirstOrDefault(u => u.Name == name);
        }

        public IReadOnlyCollection<Publisher> GetPublishers(int skip, int take)
        {
            return _context.Publisher
                .Skip(skip)
                .Take(take)
                .ToList();
        }       
    }
}