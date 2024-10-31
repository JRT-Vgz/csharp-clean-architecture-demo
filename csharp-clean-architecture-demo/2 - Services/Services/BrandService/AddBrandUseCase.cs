using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using AutoMapper;

namespace _2___Services.Services.BrandService
{
    public class AddBrandUseCase<TInsertDto, TDto>
    {
        private readonly IRepository<BrandEntity> _brandRepository;
        private readonly IMapper _mapper;
        private readonly IEntityValidator<BrandEntity> _entityValidator;

        public AddBrandUseCase(IRepository<BrandEntity> brandRepository,
            IMapper mapper,
            IEntityValidator<BrandEntity> entityValidator)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _entityValidator = entityValidator;
        }

        public async Task<TDto> ExecuteAsync(TInsertDto brandInsertDto)
        {
            var brandEntity = _mapper.Map<BrandEntity>(brandInsertDto);

            var isValid = await _entityValidator.ValidateAsync(brandEntity);
            if (!isValid) { throw new EntityValidationException(_entityValidator.Errors); }           

            brandEntity = await _brandRepository.AddAsync(brandEntity);

            return _mapper.Map<TDto>(brandEntity);
        }
    }
}
