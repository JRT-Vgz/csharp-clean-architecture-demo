using _1___Entities;
using _2___Services._Interfaces;
using _3___Presenters.ViewModels;

namespace _3___Presenters
{
    public class BeerDetailPresenter : IPresenter<BeerEntity, BeerDetailViewModel>
    {
        public BeerDetailViewModel Present(BeerEntity beerEntity)
            => new BeerDetailViewModel
            {
                Name = "Cerveza: " + beerEntity.Name,
                BrandName = "Marca: " + beerEntity.BrandName,
                Alcohol = beerEntity.Alcohol + "% de alcohol",
                Color = beerEntity.IsStrongBeer() ? "Red" : "Green",
                Message = beerEntity.IsStrongBeer() ? "Strong Beer" : "Smooth Beer"
            };
    }
}
