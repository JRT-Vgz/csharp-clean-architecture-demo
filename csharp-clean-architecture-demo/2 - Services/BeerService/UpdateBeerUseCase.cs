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
    public class UpdateBeerUseCase<TUpdateDto, TDto>
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        private readonly IMapper _mapper;

        public UpdateBeerUseCase(IRepository<BeerEntity> beerRepository,
            IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<TDto> ExecuteAsync(TUpdateDto beerUpdateDto, int id)
        {
            var beerEntity = _mapper.Map<BeerEntity>(beerUpdateDto);

            beerEntity = await _beerRepository.UpdateAsync(beerEntity, id);

            return _mapper.Map<TDto>(beerEntity);
        }
    }
}
