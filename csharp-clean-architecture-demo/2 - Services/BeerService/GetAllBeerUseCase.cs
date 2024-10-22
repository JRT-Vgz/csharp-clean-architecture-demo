using _1___Entities;
using _2___Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2___Services.BeerService
{
    public class GetAllBeerUseCase
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        public GetAllBeerUseCase(IRepository<BeerEntity> beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public async Task<IEnumerable<BeerEntity>> ExecuteAsync()
            => await _beerRepository.GetAllAsync();
    }
}
