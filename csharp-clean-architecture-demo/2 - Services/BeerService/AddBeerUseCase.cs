using _1___Entities;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.BeerService
{
    public class AddBeerUseCase<TInsertDto, TDto>
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        private readonly IMapper _mapper;

        public AddBeerUseCase(IRepository<BeerEntity> beerRepository, 
            IMapper mapper)

        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<TDto> ExecuteAsync(TInsertDto beerInsertDto)
        {
            var beerEntity = _mapper.Map<BeerEntity>(beerInsertDto);

            beerEntity = await _beerRepository.AddAsync(beerEntity);

            return _mapper.Map<TDto>(beerEntity);
        }
    }
}
