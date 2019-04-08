
namespace BookInspector.Services.Contracts
{
    using BookInspector.Data.Models;
    using System.Collections.Generic;

    public interface IPublisherService
    {
        Publisher Add(string name);

        IReadOnlyCollection<Publisher> GetPublishers();
    }
}