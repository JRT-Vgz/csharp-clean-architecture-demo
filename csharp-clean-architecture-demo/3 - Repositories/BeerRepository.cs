using _1___Entities;
using _2___Services.Interfaces;
using _3___Data;
using Microsoft.EntityFrameworkCore;

namespace _3___Repositories
{
    public class BeerRepository : IRepository<BeerEntity>
    {
        private readonly BreweryContext _breweryContext;
        public BeerRepository(BreweryContext breweryContext)
        {
            _breweryContext = breweryContext;
        }

        public async Task<IEnumerable<BeerEntity>> GetAllAsync()
        {
            var beerModels = await _breweryContext.Beers.Include("Brand").ToListAsync();

            return beerModels.Select(b => new BeerEntity
            {
                Id = b.Id,
                Name = b.Name,
                BrandName = b.Brand.Name,
                Alcohol = b.Alcohol
            });
        }

        public async Task<BeerEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BeerEntity> AddAsync(BeerEntity entity)
        {
            throw new NotImplementedException();
        }

        public BeerEntity UpdateAsync(BeerEntity entity)
        {
            throw new NotImplementedException();
        }

        public BeerEntity DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
