using _1___Entities;
using _2___Services.Interfaces;
using _3___Data;
using _3___Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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
            var beerModels = await _breweryContext.Beers.Include("Brand").OrderBy(b => b.Id).ToListAsync();

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

        public async Task<BeerEntity> UpdateAsync(BeerEntity beerEntity, int id)
        {
            var beerModel = await _breweryContext.Beers.FindAsync(id);

            _mapper.Map(beerEntity, beerModel);

            _breweryContext.Beers.Attach(beerModel);
            _breweryContext.Beers.Entry(beerModel).State = EntityState.Modified;
            await _breweryContext.SaveChangesAsync();

            await _breweryContext.Brands.FindAsync(beerModel.IdBrand);
            return _mapper.Map<BeerEntity>(beerModel);
        }

        public async Task<BeerEntity> DeleteAsync(int id)
        {
            var beerModel = await _breweryContext.Beers.Include("Brand").FirstOrDefaultAsync(b => b.Id == id);

            _breweryContext.Beers.Remove(beerModel);
            await _breweryContext.SaveChangesAsync();

            return _mapper.Map<BeerEntity>(beerModel);

        }
    }
}
