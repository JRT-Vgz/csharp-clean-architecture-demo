
namespace _2___Services._Interfaces
{
    public interface IManualMapper<TDto, TEntity>
    {
        TEntity Map(TDto dto);
    }
}
