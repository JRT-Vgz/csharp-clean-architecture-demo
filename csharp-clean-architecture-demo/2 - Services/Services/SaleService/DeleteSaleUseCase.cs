
using _1___Entities;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.Services.SaleService
{
    public class DeleteSaleUseCase<TDto>
    {
        private readonly IRepository<SaleEntity> _saleRepository;
        private readonly IMapper _mapper;

        public DeleteSaleUseCase(IRepository<SaleEntity> saleRepository,
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }
        public async Task<TDto> ExecuteAsync(int id)
        {
            var saleEntity = await _saleRepository.DeleteAsync(id);

            if (saleEntity == null) { throw new NotFoundException($"No se encontró ninguna venta con ID {id}"); }

            return _mapper.Map<TDto>(saleEntity);
        }
    }
}
