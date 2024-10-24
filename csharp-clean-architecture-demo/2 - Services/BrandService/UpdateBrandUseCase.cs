using _1___Entities;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.BrandService
{
    public class UpdateBrandUseCase<TUpdateDto, TDto>
    {
        private readonly IRepository<BrandEntity> _brandRepository;
        private readonly IMapper _mapper;
        private readonly IRequestValidator<TUpdateDto> _requestValidator;

        public UpdateBrandUseCase(IRepository<BrandEntity> brandRepository,
            IMapper mapper,
            IRequestValidator<TUpdateDto> requestValidator)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _requestValidator = requestValidator;
        }

        public async Task<TDto> ExecuteAsync(TUpdateDto brandUpdateDto, int id)
        {
            var requestValidation = await _requestValidator.Validate(brandUpdateDto);
            if (!requestValidation) { throw new RequestValidationException(_requestValidator.Errors); }

            var brandEntity = _mapper.Map<BrandEntity>(brandUpdateDto);

            brandEntity = await _brandRepository.UpdateAsync(brandEntity, id);

            return _mapper.Map<TDto>(brandEntity);
        }
    }
}
