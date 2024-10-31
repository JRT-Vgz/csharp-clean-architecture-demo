
using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.Services.ConceptService
{
    public class UpdateConceptUseCase<TUpdateDto, TDto>
    {
        private readonly IRepository<ConceptEntity> _conceptRepository;
        private readonly IRepository<SaleEntity> _saleRepository;
        private readonly IMapper _mapper;
        private readonly IEntityValidator<ConceptEntity> _entityValidator;
        public UpdateConceptUseCase(IRepository<ConceptEntity> conceptRepository, 
            IRepository<SaleEntity> saleRepository, 
            IMapper mapper,
            IEntityValidator<ConceptEntity> entityValidator)
        {
            _conceptRepository = conceptRepository;
            _saleRepository = saleRepository;
            _mapper = mapper;
            _entityValidator = entityValidator;
        }

        public async Task<TDto> ExecuteAsync(TUpdateDto conceptUpdateDto, int id)
        {
            var conceptEntity = _mapper.Map<ConceptEntity>(conceptUpdateDto);

            var resultIsValid = await _entityValidator.ValidateAsync(conceptEntity);
            if (!resultIsValid) { throw new EntityValidationException(_entityValidator.Errors); }

            var updatedConceptEntity = await _conceptRepository.UpdateAsync(conceptEntity, id);
            if (updatedConceptEntity == null) { throw new NotFoundException($"No se encontró ningún concepto con ID {id}"); }            

            var saleEntity = await _saleRepository.GetByIdAsync(updatedConceptEntity.IdSale);
            await _saleRepository.UpdateAsync(saleEntity, updatedConceptEntity.IdSale);

            return _mapper.Map<TDto>(conceptEntity);
        }
    }
}
