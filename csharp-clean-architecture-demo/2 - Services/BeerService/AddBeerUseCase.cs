using _1___Entities;
using _2___Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2___Services.BeerService
{
    public class AddBeerUseCase<TInsertDto, TDto>
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        private readonly IMapper<TInsertDto, BeerEntity> _insertDtoMapper;
        private readonly IMapper<BeerEntity, TDto> _entityMapper;

        public AddBeerUseCase(IRepository<BeerEntity> beerRepository, 
            IMapper<TInsertDto, BeerEntity> insertDtoMapper,
            IMapper<BeerEntity, TDto> entityMapper)
        {
            _beerRepository = beerRepository;
            _insertDtoMapper = insertDtoMapper;
            _entityMapper = entityMapper;
        }

        public async Task<TDto> ExecuteAsync(TInsertDto beerInsertDto)
        {
            var beerEntity = _insertDtoMapper.Map(beerInsertDto);

            beerEntity = await _beerRepository.AddAsync(beerEntity);

            var beerDto = _entityMapper.Map(beerEntity);
            return beerDto;
        }
    }
}
