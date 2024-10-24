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
    public class AddBrandUseCase<TInsertDto, TDto>
    {
        private readonly IRepository<BrandEntity> _brandRepository;
        private readonly IMapper _mapper;

        public AddBrandUseCase(IRepository<BrandEntity> brandRepository,
            IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<TDto> ExecuteAsync(TInsertDto brandInsertDto)
        {
            var brandEntity = _mapper.Map<BrandEntity>(brandInsertDto);

            brandEntity = await _brandRepository.AddAsync(brandEntity);

            return _mapper.Map<TDto>(brandEntity);
        }
    }
}
