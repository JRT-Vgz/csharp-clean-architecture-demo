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
    public class GetBeerByIdUseCase<TDto>
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        private readonly IMapper _mapper;
        public GetBeerByIdUseCase(IRepository<BeerEntity> beerRepository, 
            IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<TDto> ExecuteAsync(int id)
        {
            var beerEntity = await _beerRepository.GetByIdAsync(id);

            return _mapper.Map<TDto>(beerEntity);
        }
    }
}
