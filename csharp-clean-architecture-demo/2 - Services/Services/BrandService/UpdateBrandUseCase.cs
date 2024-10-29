using _1___Entities;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.Services.BrandService
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
            var isValid = await _requestValidator.Validate(brandUpdateDto);
            if (!isValid) { throw new RequestValidationException(_requestValidator.Errors); }

            var brandEntity = _mapper.Map<BrandEntity>(brandUpdateDto);

            var updatedBrandEntity = await _brandRepository.UpdateAsync(brandEntity, id);

            if (updatedBrandEntity == null) { throw new NotFoundException($"No se encontró ninguna marca con ID {id}"); }

            return _mapper.Map<TDto>(updatedBrandEntity);

        }
    }
}
