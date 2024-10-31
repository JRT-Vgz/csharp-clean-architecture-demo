using _1___Entities;
using _2___Services._Interfaces;
using AutoMapper;

namespace _2___Services.Services.BeerService
{
    public class GetAllBeerUseCase<TDto>
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        private readonly IMapper _mapper;
        public GetAllBeerUseCase(IRepository<BeerEntity> beerRepository,
            IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> ExecuteAsync()
        {
            var beerEntities = await _beerRepository.GetAllAsync();

            return beerEntities.Select(b => _mapper.Map<TDto>(b));
        }
    }
}
