
namespace _2___Services._Interfaces
{
    public interface IEntityValidator<TEntity>
    {
        public List<string> Errors { get; set; }
        Task<bool> Validate(TEntity entity);
    }
}
