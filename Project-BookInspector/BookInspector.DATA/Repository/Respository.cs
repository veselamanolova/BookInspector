
namespace BookInspector.Data.Repository
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BookInspector.Data.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class Repository<TSource> : IRepository<TSource> where TSource : class
    {
        private readonly BookInspectorContext _context;

        public Repository(BookInspectorContext context)
        {
            _context = context;
        }

        public IQueryable<TSource> All()
        {
            return _context.Set<TSource>();
        }

        public async Task Add(TSource entity)
        {
            EntityEntry entry = _context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }

            else
            {
                await _context.Set<TSource>().AddAsync(entity);
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Remove(TSource entity)
        {
            throw new NotImplementedException();
        }
    }
}

