
namespace _2___Services.Interfaces
{
    public interface IPresenter<TEntity, TViewModel>
    {
        TViewModel Present(TEntity entity);
    }
}
