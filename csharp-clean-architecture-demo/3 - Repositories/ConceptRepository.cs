using _1___Entities;
using _2___Services._Interfaces;
using _3___Data;
using _3___Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _3___Repositories
{
    public class ConceptRepository : IRepository<ConceptEntity>, IRepositorySearch<ConceptModel, ConceptEntity>
    {
        private readonly BreweryContext _breweryContext;
        private readonly IMapper _mapper;
        public ConceptRepository(BreweryContext breweryContext, 
            IMapper mapper)
        {
            _breweryContext = breweryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConceptEntity>> GetAllAsync()
        {
            var conceptModels = await _breweryContext.Concepts.ToListAsync();

            return conceptModels.Select(c => _mapper.Map<ConceptEntity>(c));
        }

        public async Task<ConceptEntity> GetByIdAsync(int id)
        {
            var conceptModel = await _breweryContext.Concepts.FindAsync(id);

            if (conceptModel == null) { return null; }

            return _mapper.Map<ConceptEntity>(conceptModel);
        }

        public async Task<ConceptEntity> AddAsync(ConceptEntity conceptEntity)
        {
            var conceptModel = _mapper.Map<ConceptModel>(conceptEntity);

            await _breweryContext.AddAsync(conceptModel);
            await _breweryContext.SaveChangesAsync();

            return _mapper.Map<ConceptEntity>(conceptModel);
        }

        public async Task<ConceptEntity> UpdateAsync(ConceptEntity conceptEntity, int id)
        {
            var conceptModel = await _breweryContext.Concepts.FindAsync(id);
            if (conceptModel == null) { return null; }

            conceptModel.IdBeer = conceptEntity.IdBeer;
            conceptModel.Quantity = conceptEntity.Quantity;
            conceptModel.UnitPrice = conceptEntity.UnitPrice;

            _breweryContext.Concepts.Attach(conceptModel);
            _breweryContext.Concepts.Entry(conceptModel).State = EntityState.Modified;
            await _breweryContext.SaveChangesAsync();

            return _mapper.Map<ConceptEntity>(conceptModel);
        }

        public async Task<ConceptEntity> DeleteAsync(int id)
        {
            var conceptModel = await _breweryContext.Concepts.FindAsync(id);

            if (conceptModel == null) { return null; }

            _breweryContext.Concepts.Remove(conceptModel);
            await _breweryContext.SaveChangesAsync();

            return _mapper.Map<ConceptEntity>(conceptModel);
        }

        public async Task<IEnumerable<ConceptEntity>> SearchAllAsync(Expression<Func<ConceptModel, bool>> predicate)
        {
            var conceptModels = await _breweryContext.Concepts.Where(predicate).ToListAsync();

            return conceptModels.Select(c => _mapper.Map<ConceptEntity>(c));
        }
    }
}
