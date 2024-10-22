using _1___Entities;
using _2___Services.Interfaces;
using _3___Data;
using _3___Data.Models;
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
            var beerModel = await _breweryContext.Beers.Include("Brand").FirstOrDefaultAsync(b => b.Id == id);

            return new BeerEntity
            {
                Id = beerModel.Id,
                Name = beerModel.Name,
                BrandName = beerModel.Brand.Name,
                Alcohol = beerModel.Alcohol
            };
        }

        public async Task<BeerEntity> AddAsync(BeerEntity beerEntity)
        {
            var brand = await _breweryContext.Brands.FindAsync(beerEntity.IdBrand);

            var beerModel = new BeerModel
            {
                Name = beerEntity.Name,
                IdBrand = beerEntity.IdBrand,
                Alcohol = beerEntity.Alcohol,
                Brand = brand
            };

            await _breweryContext.Beers.AddAsync(beerModel);
            await _breweryContext.SaveChangesAsync();

            return new BeerEntity
            {
                Id = beerModel.Id,
                Name = beerModel.Name,
                IdBrand = beerModel.IdBrand,
                BrandName = beerModel.Brand.Name,
                Alcohol = beerModel.Alcohol
            };

        }

        public BeerEntity UpdateAsync(BeerEntity entity)
        {
            throw new NotImplementedException();
        }

        public BeerEntity DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
