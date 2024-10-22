using _1___Entities;
using _2___Services.Interfaces;
using _3___Presenters.ViewModels;

namespace _3___Presenters
{
    public class BeerPresenter : IPresenter<BeerEntity, BeerViewModel>
    {
        public BeerViewModel Present(BeerEntity beerEntity)
            => new BeerViewModel
            {
                Name = "Cerveza: " + beerEntity.Name,
                BrandName = "Marca: " + beerEntity.BrandName,
                Alcohol = beerEntity.Alcohol + "% de alcohol"
            };
    }
}
