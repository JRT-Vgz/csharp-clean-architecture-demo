using _1___Entities;
using _2___Services._Interfaces;
using _3___Data;
using _3___Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _3___Repositories
{
    public class SaleRepository : IRepository<SaleEntity>, IRepositorySearch<SaleModel, SaleEntity>
    {
        private readonly BreweryContext _breweryContext;
        private readonly IMapper _mapper;
        public SaleRepository(BreweryContext breweryContext,
            IMapper mapper) 
        {
            _breweryContext = breweryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SaleEntity>> GetAllAsync()
        {
            var saleModels = await _breweryContext.Sales.Include("Concepts").ToListAsync();

            return saleModels.Select(s => new SaleEntity(s.Id, 
                                           s.Date, 
                                           s.Concepts.Select(c => new ConceptEntity(c.Id, c.IdBeer, c.Quantity, c.UnitPrice)).ToList()
                                           ));
        }

        public async Task<SaleEntity> GetByIdAsync(int id)
        {
            var saleModel = await _breweryContext.Sales.FindAsync(id);

            if (saleModel == null) { return null; }

            return new SaleEntity(saleModel.Id,
                    saleModel.Date,
                    await _breweryContext.Concepts.Where(c => c.IdSale == saleModel.Id)
                                            .Select(c => new ConceptEntity(c.Id, c.IdBeer, c.Quantity, c.UnitPrice))
                                            .ToListAsync());
                    
        }

        public async Task<SaleEntity> AddAsync(SaleEntity saleEntity)
        {
            var saleModel = _mapper.Map<SaleModel>(saleEntity);

            await _breweryContext.Sales.AddAsync(saleModel);
            await _breweryContext.SaveChangesAsync();

            return _mapper.Map<SaleEntity>(saleModel);
        }
        public async Task<SaleEntity> UpdateAsync(SaleEntity saleEntity, int id)
        {
            var saleModel = await _breweryContext.Sales.FindAsync(id);

            _mapper.Map(saleEntity, saleModel);

            _breweryContext.Sales.Attach(saleModel);
            _breweryContext.Sales.Entry(saleModel).State = EntityState.Modified;
            await _breweryContext.SaveChangesAsync();

            return _mapper.Map<SaleEntity>(saleModel);

        }

        public async Task<SaleEntity> DeleteAsync(int id)
        {
            var saleModel = await _breweryContext.Sales.Include("Concepts").FirstOrDefaultAsync(s => s.Id == id);

            if (saleModel == null) { return null; }

            _breweryContext.Sales.Remove(saleModel);
            await _breweryContext.SaveChangesAsync();

            return _mapper.Map<SaleEntity>(saleModel);
        }

        public async Task<IEnumerable<SaleEntity>> SearchAllAsync(Expression<Func<SaleModel, bool>> predicate)
        {
            var saleModels = await _breweryContext.Sales.Where(predicate).Include("Concepts").ToListAsync();

            var saleEntities = new List<SaleEntity>();

            foreach (var saleModel in saleModels)
            {
                var conceptEntities = new List<ConceptEntity>();

                foreach (var conceptModel in saleModel.Concepts)
                {
                    conceptEntities.Add(new ConceptEntity(conceptModel.Id, conceptModel.IdBeer, conceptModel.Quantity, conceptModel.UnitPrice));
                }

                saleEntities.Add(new SaleEntity(saleModel.Id, saleModel.Date, conceptEntities));
            }

            return saleEntities;
        }
    }
}
