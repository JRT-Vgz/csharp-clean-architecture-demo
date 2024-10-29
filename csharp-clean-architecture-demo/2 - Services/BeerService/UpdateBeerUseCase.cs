using _1___Entities;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.BeerService
{
    public class UpdateBeerUseCase<TUpdateDto, TDto>
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        private readonly IMapper _mapper;
        private readonly IRequestValidator<TUpdateDto> _requestValidator;

        public UpdateBeerUseCase(IRepository<BeerEntity> beerRepository,
            IMapper mapper,
            IRequestValidator<TUpdateDto> requestValidator)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            _requestValidator = requestValidator;
        }

        public async Task<TDto> ExecuteAsync(TUpdateDto beerUpdateDto, int id)
        {
            var isValid = await _requestValidator.Validate(beerUpdateDto);
            if (!isValid) { throw new RequestValidationException(_requestValidator.Errors); }

            var beerEntity = _mapper.Map<BeerEntity>(beerUpdateDto);

            var updatedBeerEntity = await _beerRepository.UpdateAsync(beerEntity, id);

            if (updatedBeerEntity == null) { throw new NotFoundException($"No se encontró ninguna cerveza con ID {id}"); }

            return _mapper.Map<TDto>(updatedBeerEntity);
        }
    }
}
