
using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace _2___Services.Services.SaleService
{
    public class AddSaleUseCase<TInsertDto, TDto>
    {
        private readonly IRepository<SaleEntity> _saleRepository;
        private readonly IManualMapper<TInsertDto, SaleEntity> _manualMapper;
        private readonly IMapper _mapper;
        private readonly IEntityValidator<SaleEntity> _entityValidator;
        public AddSaleUseCase(IRepository<SaleEntity> saleRepository,
            IManualMapper<TInsertDto, SaleEntity> manualMapper,
            IMapper mapper,
            IEntityValidator<SaleEntity> entityValidator)
        {
            _saleRepository = saleRepository;
            _manualMapper = manualMapper;
            _mapper = mapper;
            _entityValidator = entityValidator;
        }

        public async Task<TDto> ExecuteAsync(TInsertDto saleInsertDto)
        {
            var saleEntity = _manualMapper.Map(saleInsertDto);

            var isEntityValid = await _entityValidator.Validate(saleEntity);
            if (!isEntityValid) { throw new EntityValidationException(_entityValidator.Errors); }

            var insertedSaleEntity = await _saleRepository.AddAsync(saleEntity);

            return _mapper.Map<TDto>(insertedSaleEntity);
        }
    }
}
