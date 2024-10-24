using _1___Entities;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.BrandService
{
    public class DeleteBrandUseCase<TDto>
    {
        private readonly IRepository<BrandEntity> _brandRepository;
        private readonly IMapper _mapper;

        public DeleteBrandUseCase(IRepository<BrandEntity> brandRepository,
            IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<TDto> ExecuteAsync(int id)
        {
            var brandEntity = await _brandRepository.DeleteAsync(id);

            return _mapper.Map<TDto>(brandEntity);
        }
    }
}
