
using _1___Entities;
using _2___Services._Exceptions;
using _2___Services._Interfaces;
using AutoMapper;

namespace _2___Services.Services.ConceptService
{
    public class GetConceptByIdUseCase<TDto>
    {
        private IRepository<ConceptEntity> _conceptRepository;
        private IMapper _mapper;
        public GetConceptByIdUseCase(IRepository<ConceptEntity> conceptRepository, 
            IMapper mapper)
        {
            _conceptRepository = conceptRepository;
            _mapper = mapper;
        }

        public async Task<TDto> ExecuteAsync(int id)
        {
            var conceptEntity = await _conceptRepository.GetByIdAsync(id);

            if (conceptEntity == null) { throw new NotFoundException($"No se encontró ningún concepto con ID {id}"); }

            return _mapper.Map<TDto>(conceptEntity);
        }
    }
}
