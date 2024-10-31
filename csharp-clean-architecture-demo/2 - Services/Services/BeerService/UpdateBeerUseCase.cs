using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.Services.BeerService
{
    public class UpdateBeerUseCase<TUpdateDto, TDto>
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        private readonly IMapper _mapper;
        private readonly IEntityValidator<BeerEntity> _entityValidator;

        public UpdateBeerUseCase(IRepository<BeerEntity> beerRepository,
            IMapper mapper,
            IEntityValidator<BeerEntity> entityValidator)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            _entityValidator = entityValidator;
        }

        public async Task<TDto> ExecuteAsync(TUpdateDto beerUpdateDto, int id)
        {
            var beerEntity = _mapper.Map<BeerEntity>(beerUpdateDto);

            var isValid = await _entityValidator.ValidateAsync(beerEntity);
            if (!isValid) { throw new EntityValidationException(_entityValidator.Errors); }           

            var updatedBeerEntity = await _beerRepository.UpdateAsync(beerEntity, id);
            if (updatedBeerEntity == null) { throw new NotFoundException($"No se encontró ninguna cerveza con ID {id}"); }

            return _mapper.Map<TDto>(updatedBeerEntity);
        }
    }
}
