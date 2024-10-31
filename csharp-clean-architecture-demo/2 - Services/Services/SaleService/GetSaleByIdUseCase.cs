
using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using AutoMapper;

namespace _2___Services.Services.SaleService
{
    public class GetSaleByIdUseCase<TDto>
    {
        private readonly IRepository<SaleEntity> _saleRepository;
        private readonly IMapper _mapper;
        public GetSaleByIdUseCase(IRepository<SaleEntity> saleRepository,
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<TDto> ExecuteAsync(int id)
        {
            var saleEntity = await _saleRepository.GetByIdAsync(id);

            if (saleEntity == null) { throw new NotFoundException($"No se encontró ninguna venta con ID {id}"); }

            return _mapper.Map<TDto>(saleEntity);
        }
    }
}
