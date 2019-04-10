
namespace BookInspector.Services.Repository
{
    using System.Linq;
    using System.Threading.Tasks;
    using BookInspector.Data.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        public async Task AddAsync(T entity)
        {
            EntityEntry entry = _context.Entry(entity);

            if (entry.State != EntityState.Detached)
                entry.State = EntityState.Added;

            else await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            EntityEntry entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
                _context.Set<T>().Attach(entity);

            entry.State = EntityState.Modified;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        void IRepository<T>.Add(T entity)
        {
            EntityEntry entry = _context.Entry(entity);

            if (entry.State != EntityState.Detached)
                entry.State = EntityState.Added;

            else _context.Set<T>().Add(entity);
        }

        void IRepository<T>.Save()
        {
            _context.SaveChanges();
        }
    }
}
