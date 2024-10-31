
namespace _3____Adapters.Interfaces
{
    public interface IExternalService<TDto>
    {
        Task<IEnumerable<TDto>> GetAllContentAsync();
        Task<TDto> GetContentByIdAsync(int id);
    }
}
