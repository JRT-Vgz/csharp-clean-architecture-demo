
using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.Services.ConceptService
{
    public class AddConceptToIdSaleUseCase<TInsertDto, TDto>
    {
        private readonly IRepository<ConceptEntity> _conceptRepository;
        private readonly IRepository<SaleEntity> _saleRepository;
        private readonly IMapper _mapper;
        private readonly IEntityValidator<ConceptEntity> _entityValidator;
        public AddConceptToIdSaleUseCase(IRepository<ConceptEntity> conceptRepository,
            IRepository<SaleEntity> saleRepository,
            IMapper mapper,
            IEntityValidator<ConceptEntity> entityValidator)
        {
            _conceptRepository = conceptRepository;
            _saleRepository = saleRepository;
            _mapper = mapper;
            _entityValidator = entityValidator;
        }

        public async Task<TDto> ExecuteAsync(TInsertDto conceptInsertToIdSaleDto, int idSale)
        {
            var conceptEntity = _mapper.Map<ConceptEntity>(conceptInsertToIdSaleDto);

            if (conceptEntity.IdSale == 0 || idSale == 0) { throw new NotFoundException("No se encontró ninguna venta con ID 0"); }

            var resultIsValid = await _entityValidator.ValidateAsync(conceptEntity);
            if (!resultIsValid) { throw new EntityValidationException(_entityValidator.Errors); }

            var insertedConceptEntity = await _conceptRepository.AddAsync(conceptEntity);

            var saleEntity = await _saleRepository.GetByIdAsync(insertedConceptEntity.IdSale);
            await _saleRepository.UpdateAsync(saleEntity, insertedConceptEntity.IdSale);

            return _mapper.Map<TDto>(saleEntity);
        }
    }
}
