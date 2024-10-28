using _1___Entities;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.BeerService
{
    public class DeleteBeerUseCase<TDto>
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        private readonly IMapper _mapper;

        public DeleteBeerUseCase(IRepository<BeerEntity> beerRepository,
            IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }
        public async Task<TDto> ExecuteAsync(int id)
        {
            var beerEntity = await _beerRepository.DeleteAsync(id);

            if (beerEntity == null) { throw new NotFoundException($"No se encontró ninguna cerveza con ID {id}"); }

            return _mapper.Map<TDto>(beerEntity);
        }
    }
}
