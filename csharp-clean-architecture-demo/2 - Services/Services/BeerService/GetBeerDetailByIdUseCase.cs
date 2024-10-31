using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;

namespace _2___Services.Services.BeerService
{
    public class GetBeerDetailByIdUseCase<TViewModel>
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        private readonly IPresenter<BeerEntity, TViewModel> _presenter;
        public GetBeerDetailByIdUseCase(IRepository<BeerEntity> beerRepository,
            IPresenter<BeerEntity, TViewModel> presenter)
        {
            _beerRepository = beerRepository;
            _presenter = presenter;
        }

        public async Task<TViewModel> ExecuteAsync(int id)
        {
            var beerEntity = await _beerRepository.GetByIdAsync(id);

            if (beerEntity == null) { throw new NotFoundException($"No se encontró ninguna cerveza con ID {id}"); }

            return _presenter.Present(beerEntity);
        }
    }
}
