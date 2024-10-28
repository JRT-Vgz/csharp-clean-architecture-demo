using _1___Entities;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.BeerService
{
    public class AddBeerUseCase<TInsertDto, TDto>
    {
        private readonly IRepository<BeerEntity> _beerRepository;
        private readonly IMapper _mapper;
        private readonly IRequestValidator<TInsertDto> _requestValidator;

        public AddBeerUseCase(IRepository<BeerEntity> beerRepository, 
            IMapper mapper,
            IRequestValidator<TInsertDto> requestValidator)

        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            _requestValidator = requestValidator;
        }

        public async Task<TDto> ExecuteAsync(TInsertDto beerInsertDto)
        {
            var result = await _requestValidator.Validate(beerInsertDto);
            if (!result) { throw new RequestValidationException(_requestValidator.Errors); }

            var beerEntity = _mapper.Map<BeerEntity>(beerInsertDto);

            beerEntity = await _beerRepository.AddAsync(beerEntity);

            return _mapper.Map<TDto>(beerEntity);
        }
    }
}
