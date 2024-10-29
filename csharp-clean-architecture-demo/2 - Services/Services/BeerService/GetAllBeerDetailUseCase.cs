using _1___Entities;
using _2___Services.Interfaces;

namespace _2___Services.Services.BeerService
{
    public class GetAllBeerDetailUseCase<TViewModel>
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        private readonly IPresenter<BeerEntity, TViewModel> _presenter;
        public GetAllBeerDetailUseCase(IRepository<BeerEntity> beerRepository,
            IPresenter<BeerEntity, TViewModel> presenter)
        {
            _beerRepository = beerRepository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TViewModel>> ExecuteAsync()
        {
            var beerEntities = await _beerRepository.GetAllAsync();

            return beerEntities.Select(b => _presenter.Present(b));
        }
    }
}
