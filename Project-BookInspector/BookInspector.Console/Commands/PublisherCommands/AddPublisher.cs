
namespace BookInspector.Console.Commands
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class AddPublisher : ICommand
    {
        private readonly IPublisherService publisherService;

        public AddPublisher(IPublisherService publisherService)
        {
            this.publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (!args.Any())
            {
                throw new ArgumentException("Please provide a publisher mame as first parameter");
            }

            var publisher = this.publisherService.Add(args[0]);

            return $"Publisher {publisher.Name}, Id = {publisher.PublisherId} added";
        }

    }
}

