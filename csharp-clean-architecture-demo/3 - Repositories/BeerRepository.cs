using _1___Entities;
using _2___Services.Interfaces;
using _3___Data;
using _3___Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _3___Repositories
{
    public class BeerRepository : IRepository<BeerEntity>
    {
        private readonly BreweryContext _breweryContext;
        private readonly IMapper _mapper;
        public BeerRepository(BreweryContext breweryContext,
            IMapper mapper)
        {
            _breweryContext = breweryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BeerEntity>> GetAllAsync()
        {
            var beerModels = await _breweryContext.Beers.Include("Brand").ToListAsync();

            return beerModels.Select(b => _mapper.Map<BeerEntity>(b));
        }

        public async Task<BeerEntity> GetByIdAsync(int id)
        {
            var beerModel = await _breweryContext.Beers.Include("Brand").FirstOrDefaultAsync(b => b.Id == id);

            return _mapper.Map<BeerEntity>(beerModel);
        }

        public async Task<BeerEntity> AddAsync(BeerEntity beerEntity)
        {
            await _breweryContext.Brands.FindAsync(beerEntity.IdBrand);
            var beerModel = _mapper.Map<BeerModel>(beerEntity);

            await _breweryContext.Beers.AddAsync(beerModel);
            await _breweryContext.SaveChangesAsync();

            return _mapper.Map<BeerEntity>(beerModel);

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
