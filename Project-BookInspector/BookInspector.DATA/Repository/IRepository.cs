
namespace BookInspector.Data.Repository
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TSource> where TSource : class
    {
        IQueryable<TSource> All();

        Task Add(TSource entity);

        Task Save();

        void Remove(TSource entity);
    }
}

