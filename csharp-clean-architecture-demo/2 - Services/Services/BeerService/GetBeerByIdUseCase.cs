using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using AutoMapper;

namespace _2___Services.Services.BeerService
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

            if (beerEntity == null) { throw new NotFoundException($"No se encontró ninguna cerveza con ID {id}"); }

            return _mapper.Map<TDto>(beerEntity);
        }
    }
}
