
namespace BookInspector.App.Commands
{
    using System;
    using System.Linq;
    using BookInspector.Services;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class ShowAllPublishers : ICommand
    {
        private readonly IPublisherService _publisherService;

        public ShowAllPublishers(IPublisherService categoryService)
        {
            _publisherService = categoryService ?? throw new ArgumentNullException(nameof(CategoryService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            return string.Join(", ", _publisherService.GetPublishers().Select(x => x.Name));
        }
    }
}
