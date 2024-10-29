
using _1___Entities;
using _2___Services._Interfaces;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace _2___Services.SaleService
{
    public class AddSaleUseCase<TInsertDto>
    {
        private IRepository<SaleEntity> _saleRepository;
        private IManualMapper<TInsertDto, SaleEntity> _mapper;
        private IRequestValidator<TInsertDto> _requestValidator;
        public AddSaleUseCase(IRepository<SaleEntity> saleRepository, 
            IManualMapper<TInsertDto, SaleEntity> mapper,
            IRequestValidator<TInsertDto> requestValidator)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _requestValidator = requestValidator;
        }

        public async Task ExecuteAsync(TInsertDto saleInsertDto)
        {
            var isValid = await _requestValidator.Validate(saleInsertDto);
            if (!isValid) { throw new RequestValidationException(_requestValidator.Errors); }

            var saleEntity = _mapper.Map(saleInsertDto);

            //// Reglas de negocio
            //if (sale.Concepts.Count == 0)
            //    throw new ValidationException("Una venta debe tener conceptos.");

            //if (sale.Total <= 0)
            //    throw new ValidationException("Una venta debe tener más de $ 0.00 en Total.");

            await _saleRepository.AddAsync(saleEntity);
        }
    }
}
