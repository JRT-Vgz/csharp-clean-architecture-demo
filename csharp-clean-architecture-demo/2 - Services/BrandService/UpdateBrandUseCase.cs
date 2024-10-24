using _1___Entities;
using _2___Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2___Services.BrandService
{
    public class UpdateBrandUseCase<TUpdateDto, TDto>
    {
        private readonly IRepository<BrandEntity> _brandRepository;
        private readonly IMapper _mapper;

        public UpdateBrandUseCase(IRepository<BrandEntity> brandRepository,
            IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<TDto> ExecuteAsync(TUpdateDto brandUpdateDto, int id)
        {
            var brandEntity = _mapper.Map<BrandEntity>(brandUpdateDto);

            brandEntity = await _brandRepository.UpdateAsync(brandEntity, id);

            return _mapper.Map<TDto>(brandEntity);
        }
    }
}
