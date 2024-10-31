using System.Linq.Expressions;

namespace _2___Services._Interfaces
{
    public interface IRepositorySearch<TModel, TEntity>
    {
        Task<IEnumerable<TEntity>> SearchAllAsync(Expression<Func<TModel, bool>> predicate);
    }
}
