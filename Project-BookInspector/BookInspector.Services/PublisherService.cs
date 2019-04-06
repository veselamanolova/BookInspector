namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using System.Collections.Generic;
    using BookInspector.Data.Context;
    using BookInspector.Services.Contracts;

    public class PublisherService : IPublisherService
    {

        private readonly BookInspectorContext context;

        public PublisherService(BookInspectorContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Publisher Add(string name)
        {

            if (this.context.Publisher.Any(u => u.Name == name))
            {
                throw new ArgumentException($"Publisher {name} already exists");
            }

            var publisher = new Publisher() { Name = name };
            this.context.Publisher.Add(publisher);
            this.context.SaveChanges();

            return publisher;
        }

        public Publisher FindByName(string name)
        {
            return this.context.Publisher
                .FirstOrDefault(u => u.Name == name);
        }

        public IReadOnlyCollection<Publisher> GetPublishers(int skip, int take)
        {
            return this.context.Publisher
                .Skip(skip)
                .Take(take)
                .ToList();
        }       
      
    }
}