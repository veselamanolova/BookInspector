
namespace BookInspector.Services.Repository
{
    using System.Linq;
    using BookInspector.Data.Context;

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BookInspectorContext _context;

        public Repository(BookInspectorContext context)
        {
            _context = context;
        }

        public IQueryable<T> All()
        {
            return _context.Set<T>();
        }
    }
}
