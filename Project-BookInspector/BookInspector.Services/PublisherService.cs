
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.Data.Models;
    using BookInspector.Data.Repository;
    using BookInspector.Services.Contracts;

    public class PublisherService : IPublisherService
    {
        private readonly IRepository<Publisher> _publisherRepository;

        public PublisherService(IRepository<Publisher> publisherRepository)
        {
            _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository));
        }

        public Publisher Add(string name)
        {
            var publisher = _publisherRepository.All().Where(x => x.Name.Equals(name)).SingleOrDefault();
            Validator.IfNotNull<ArgumentException>(publisher, $"Publisher {name} already exists");

            publisher = new Publisher() { Name = name };
            _publisherRepository.Add(publisher);
            _publisherRepository.Save();

            return publisher;
        }

        public IReadOnlyCollection<Publisher> GetPublishers()
        {
            return _publisherRepository.All().ToList();
        }       
    }
}
