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
    public class GetAllBrandUseCase<TDto>
    {
        private readonly IRepository<BrandEntity> _brandRepository;
        private readonly IMapper _mapper;

        public GetAllBrandUseCase(IRepository<BrandEntity> brandRepository, 
            IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> ExecuteAsync()
        {
            var brandEntities = await _brandRepository.GetAllAsync();

            return brandEntities.Select(b => _mapper.Map<TDto>(b));
        }
    }
}
