using _1___Entities;
using _2___Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2___Services.BeerService
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

            return _presenter.Present(beerEntity);
        }
    }
}
