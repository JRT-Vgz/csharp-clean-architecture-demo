
using _1___Entities;
using _2___Services.Interfaces;
using AutoMapper;

namespace _2___Services.Services.ConceptService
{
    public class GetAllConceptUseCase<TDto>
    {
        private readonly IRepository<ConceptEntity> _conceptRepository;
        private readonly IMapper _mapper;
        public GetAllConceptUseCase(IRepository<ConceptEntity> conceptRepository,
            IMapper mapper)
        {
            _conceptRepository = conceptRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> ExecuteAsync()
        {
            var conceptEntities = await _conceptRepository.GetAllAsync();

            return conceptEntities.Select(c => _mapper.Map<TDto>(c));
        }
    }
}
