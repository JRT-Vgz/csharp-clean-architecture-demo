namespace _2___Services._Interfaces
{
    public interface IPresenter<TEntity, TViewModel>
    {
        TViewModel Present(TEntity entity);
    }
}
