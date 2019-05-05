
namespace BookInspector.SERVICES.Contracts
{    
    using BookInspector.DATA.Models;
    using System.Collections.Generic;

    public interface IPublisherService
    {
        Publisher Add(string name);

        IReadOnlyCollection<Publisher> GetPublishers();
    }
}