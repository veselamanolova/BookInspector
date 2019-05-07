
namespace BookInspector.SERVICES
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.DATA.Models;
    using BookInspector.SERVICES.Contracts;
    using BookInspector.SERVICES;
    using BookInspector.DATA;

    public class PublisherService : IPublisherService
    {
        private readonly ApplicationDbContext _context;

        public PublisherService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Publisher Add(string name)
        {
            var publisher = _context.Publishers.Where(x => x.PublisherName.Equals(name)).SingleOrDefault();
            Validator.IfNotNull<ArgumentException>(publisher, $"Publisher {name} already exists");

            publisher = new Publisher() { PublisherName = name };
            _context.Publishers.Add(publisher);
            _context.SaveChanges();

            return publisher;
        }

        public IReadOnlyCollection<Publisher> GetPublishers()
        {
            return _context.Publishers.ToList();
        }       
    }
}
