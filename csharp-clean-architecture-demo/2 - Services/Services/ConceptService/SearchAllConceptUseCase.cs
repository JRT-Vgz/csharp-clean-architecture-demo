
using _1___Entities;
using _2___Services._Interfaces;
using AutoMapper;
using System.Linq.Expressions;

namespace _2___Services.Services.ConceptService
{
    public class SearchAllConceptUseCase<TModel, TDto>
    {
        private IRepositorySearch<TModel, ConceptEntity> _conceptRepository;
        private IMapper _mapper;
        public SearchAllConceptUseCase(IRepositorySearch<TModel, ConceptEntity> conceptRepository, 
            IMapper mapper)
        {
            _conceptRepository = conceptRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> ExecuteAsync(Expression<Func<TModel, bool>> predicate)
        {
            var conceptEntities = await _conceptRepository.SearchAllAsync(predicate);

            return conceptEntities.Select(c => _mapper.Map<TDto>(c));
        }
    }
}
