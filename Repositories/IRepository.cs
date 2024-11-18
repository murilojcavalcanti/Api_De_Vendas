using System.Linq.Expressions;

namespace vendasApi.Repositories
{
    public interface IRepository <T>
    {
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> predicate);

        T Create(T Entity);
        T Update(T Entity);
        T Delete(T Entity);

    }
}
