
using _1___Entities;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.Services.SaleService
{
    public class GetAllSaleUseCase<TDto>
    {
        private readonly IRepository<SaleEntity> _saleRepository;
        private readonly IMapper _mapper;
        public GetAllSaleUseCase(IRepository<SaleEntity> saleRepository,
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> ExecuteAsync()
        {
            var saleEntities =  await _saleRepository.GetAllAsync();

            return saleEntities.Select(s => _mapper.Map<TDto>(s));
        }
    }
}
