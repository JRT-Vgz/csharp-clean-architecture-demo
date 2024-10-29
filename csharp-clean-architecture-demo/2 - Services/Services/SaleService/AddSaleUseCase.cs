
using _1___Entities;
using _2___Services._Interfaces;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace _2___Services.Services.SaleService
{
    public class AddSaleUseCase<TInsertDto>
    {
        private readonly IRepository<SaleEntity> _saleRepository;
        private readonly IManualMapper<TInsertDto, SaleEntity> _mapper;
        private readonly IRequestValidator<TInsertDto> _requestValidator;
        private readonly IEntityValidator<SaleEntity> _entityValidator;
        public AddSaleUseCase(IRepository<SaleEntity> saleRepository,
            IManualMapper<TInsertDto, SaleEntity> mapper,
            IRequestValidator<TInsertDto> requestValidator,
            IEntityValidator<SaleEntity> entityValidator)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _requestValidator = requestValidator;
            _entityValidator = entityValidator;
        }

        public async Task ExecuteAsync(TInsertDto saleInsertDto)
        {
            var isRequestValid = await _requestValidator.Validate(saleInsertDto);
            if (!isRequestValid) { throw new RequestValidationException(_requestValidator.Errors); }

            var saleEntity = _mapper.Map(saleInsertDto);

            var isEntityValid = await _entityValidator.Validate(saleEntity);
            if (!isEntityValid) { throw new RequestValidationException(_entityValidator.Errors); }

            await _saleRepository.AddAsync(saleEntity);
        }
    }
}
