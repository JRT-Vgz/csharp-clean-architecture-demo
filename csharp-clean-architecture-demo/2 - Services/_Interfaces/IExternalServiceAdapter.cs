namespace _2___Services._Interfaces
{
    public interface IExternalServiceAdapter<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllExternalDataAsync();
        Task<TEntity> GetDataByIdAsync(int id);
    }
}
