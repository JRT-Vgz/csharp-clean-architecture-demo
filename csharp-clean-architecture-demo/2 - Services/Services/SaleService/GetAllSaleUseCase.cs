
using _1___Entities;
using _2___Services.Interfaces;

namespace _2___Services.Services.SaleService
{
    public class GetAllSaleUseCase
    {
        private readonly IRepository<SaleEntity> _saleRepository;
        public GetAllSaleUseCase(IRepository<SaleEntity> saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<IEnumerable<SaleEntity>> ExecuteAsync()
        {
            return await _saleRepository.GetAllAsync();
        }
    }
}
