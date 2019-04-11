
namespace BookInspector.Services.Repository
{
    using System.Linq;

    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();
    }
}
