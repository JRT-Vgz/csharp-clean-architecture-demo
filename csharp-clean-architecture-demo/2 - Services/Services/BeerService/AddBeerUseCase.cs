using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.Services.BeerService
{
    public class AddBeerUseCase<TInsertDto, TDto>
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        private readonly IMapper _mapper;
        private readonly IEntityValidator<BeerEntity> _entityValidator;
        public AddBeerUseCase(IRepository<BeerEntity> beerRepository,
            IMapper mapper,
            IEntityValidator<BeerEntity> entityValidator)

        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            _mapper = mapper;
            _entityValidator = entityValidator;
        }

        public async Task<TDto> ExecuteAsync(TInsertDto beerInsertDto)
        {
            var beerEntity = _mapper.Map<BeerEntity>(beerInsertDto);

            var isValid = await _entityValidator.ValidateAsync(beerEntity);
            if (!isValid) { throw new EntityValidationException(_entityValidator.Errors); }

            var insertedBeerEntity = await _beerRepository.AddAsync(beerEntity);

            return _mapper.Map<TDto>(insertedBeerEntity);
        }
    }
}
