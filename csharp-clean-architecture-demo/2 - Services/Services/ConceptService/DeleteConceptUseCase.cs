
using _1___Entities;
using _2___Services.Exceptions;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.Services.ConceptService
{
    public class DeleteConceptUseCase<TDto>
    {
        private readonly IRepository<ConceptEntity> _conceptRepository;
        private readonly IRepository<SaleEntity> _saleRepository;
        private readonly IMapper _mapper;

        public DeleteConceptUseCase(IRepository<ConceptEntity> conceptRepository,
            IRepository<SaleEntity> saleRepository,
            IMapper mapper)
        {
            _conceptRepository = conceptRepository;
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<TDto> ExecuteAsync(int id)
        {
            var conceptEntity = await _conceptRepository.DeleteAsync(id);

            if (conceptEntity == null) { throw new NotFoundException($"No se encontró ningún concepto con ID {id}"); }

            var saleEntity = await _saleRepository.GetByIdAsync(conceptEntity.IdSale);
            var updatedSaleEntity = await _saleRepository.UpdateAsync(saleEntity, saleEntity.Id);

            if (updatedSaleEntity.Concepts.Count == 0) { await _saleRepository.DeleteAsync(updatedSaleEntity.Id); }

            return _mapper.Map<TDto>(conceptEntity);
        }
    }
}
