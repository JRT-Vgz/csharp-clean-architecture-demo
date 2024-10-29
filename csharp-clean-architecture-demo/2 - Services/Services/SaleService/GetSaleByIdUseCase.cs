
using _1___Entities;
using _2___Services.Interfaces;

namespace _2___Services.Services.SaleService
{
    public class GetSaleByIdUseCase
    {
        private readonly IRepository<SaleEntity> _saleRepository;
        public GetSaleByIdUseCase(IRepository<SaleEntity> saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<SaleEntity> ExecuteAsync(int id)
        {
            return await _saleRepository.GetByIdAsync(id);
        }
    }
}
