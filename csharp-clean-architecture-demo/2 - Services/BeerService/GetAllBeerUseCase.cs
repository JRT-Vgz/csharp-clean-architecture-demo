using _1___Entities;
using _2___Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2___Services.BeerService
{
    public class GetAllBeerUseCase<TViewModel>
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        private readonly IPresenter<BeerEntity, TViewModel> _presenter;
        public GetAllBeerUseCase(IRepository<BeerEntity> beerRepository,
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
