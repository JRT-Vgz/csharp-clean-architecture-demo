using _1___Entities;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.BrandService
{
    public class AddBrandUseCase<TInsertDto, TDto>
    {
        private readonly IRepository<BrandEntity> _brandRepository;
        private readonly IMapper _mapper;
        private readonly IRequestValidator<TInsertDto> _requestValidator;

        public AddBrandUseCase(IRepository<BrandEntity> brandRepository,
            IMapper mapper,
            IRequestValidator<TInsertDto> requestValidator)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _requestValidator = requestValidator;
        }

        public async Task<TDto> ExecuteAsync(TInsertDto brandInsertDto)
        {
            var result = await _requestValidator.Validate(brandInsertDto);
            if (!result) { throw new RequestValidationException(_requestValidator.Errors); }

            var brandEntity = _mapper.Map<BrandEntity>(brandInsertDto);

            brandEntity = await _brandRepository.AddAsync(brandEntity);

            return _mapper.Map<TDto>(brandEntity);
        }
    }
}
