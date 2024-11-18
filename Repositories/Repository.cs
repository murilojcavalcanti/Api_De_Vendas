using ApiVendasApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace vendasApi.Repositories
{
    public class Repository<T>:IRepository<T> where T : class
    {
        protected readonly ApiVendasContext _context;

        public Repository(ApiVendasContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        
        public T Create(T Entity)
        {
            _context.Set<T>().Add(Entity);
            return Entity;
        }
        /// <inheritdoc/>
        
        public T Update(T Entity)
        {
            _context.Entry(Entity).State= EntityState.Modified;
            return Entity;
        }

        public T Delete(T Entity)
        {
            _context.Set<T>().Remove(Entity);
            return Entity;
        }

    }
}
