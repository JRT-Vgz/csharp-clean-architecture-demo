
namespace _2___Services.Interfaces
{
    public interface IExternalServiceAdapter<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllExternalDataAsync();
        Task<TEntity> GetDataByIdAsync(int id);
    }
}
