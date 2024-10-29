
using _1___Entities;
using _2___Services._Interfaces;
using _2___Services.Interfaces;
using _3___Data;
using _3___Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _3___Repositories
{
    public class SaleRepository : IRepository<SaleEntity>, IRepositorySearch<SaleModel, SaleEntity>
    {
        private readonly BreweryContext _breweryContext;
        public SaleRepository(BreweryContext breweryContext) 
        {
            _breweryContext = breweryContext;
        }

        public async Task<IEnumerable<SaleEntity>> GetAllAsync()
        {
            var saleModels = await _breweryContext.Sales.Include("Concepts").ToListAsync();

            return saleModels.Select(s => new SaleEntity(s.Id, 
                                           s.Date, 
                                           s.Concepts.Select(c => new ConceptEntity(c.IdBeer, c.Quantity, c.UnitPrice)).ToList()
                                           ));
        }

        public async Task<SaleEntity> GetByIdAsync(int id)
        {
            var saleModel = await _breweryContext.Sales.FindAsync(id);

            return new SaleEntity(saleModel.Id,
                    saleModel.Date,
                    await _breweryContext.Concepts.Where(c => c.IdSale == saleModel.Id)
                                            .Select(c => new ConceptEntity(c.IdBeer, c.Quantity, c.UnitPrice))
                                            .ToListAsync());
                    
        }

        public async Task<SaleEntity> AddAsync(SaleEntity saleEntity)
        {
            var saleModel = new SaleModel
            {
                Date = saleEntity.Date,
                Total = saleEntity.Total,
                Concepts = saleEntity.Concepts.Select(c => new ConceptModel
                {
                    IdBeer = c.IdBeer,
                    Quantity = c.Quantity,
                    UnitPrice = c.UnitPrice
                }).ToList()
            };

            await _breweryContext.Sales.AddAsync(saleModel);
            await _breweryContext.SaveChangesAsync();

            return saleEntity;
        }
        public async Task<SaleEntity> UpdateAsync(SaleEntity entity, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SaleEntity> DeleteAsync(int id)
        {
            throw new NotImplementedException();
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
                    conceptEntities.Add(new ConceptEntity(conceptModel.IdBeer, conceptModel.Quantity, conceptModel.UnitPrice));
                }

                saleEntities.Add(new SaleEntity(saleModel.Id, saleModel.Date, conceptEntities));
            }

            return saleEntities;
        }
    }
}
