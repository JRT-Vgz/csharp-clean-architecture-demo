using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.Services.BrandService
{
    public class UpdateBrandUseCase<TUpdateDto, TDto>
    {
        private readonly IRepository<BrandEntity> _brandRepository;
        private readonly IMapper _mapper;
        private readonly IEntityValidator<BrandEntity> _entityValidator;

        public UpdateBrandUseCase(IRepository<BrandEntity> brandRepository,
            IMapper mapper,
            IEntityValidator<BrandEntity> entityValidator)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _entityValidator = entityValidator;
        }

        public async Task<TDto> ExecuteAsync(TUpdateDto brandUpdateDto, int id)
        {
            var brandEntity = _mapper.Map<BrandEntity>(brandUpdateDto);

            var isValid = await _entityValidator.Validate(brandEntity);
            if (!isValid) { throw new EntityValidationException(_entityValidator.Errors); }

            var updatedBrandEntity = await _brandRepository.UpdateAsync(brandEntity, id);

            if (updatedBrandEntity == null) { throw new NotFoundException($"No se encontró ninguna marca con ID {id}"); }

            return _mapper.Map<TDto>(updatedBrandEntity);

        }
    }
}
