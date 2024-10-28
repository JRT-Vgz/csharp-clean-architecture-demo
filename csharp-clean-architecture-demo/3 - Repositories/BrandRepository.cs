using _1___Entities;
using _2___Services.Interfaces;
using _3___Data;
using _3___Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _3___Repositories
{
    public class BrandRepository : IRepository<BrandEntity>
    {
        private readonly BreweryContext _breweryContext;
        private readonly IMapper _mapper;

        public BrandRepository(BreweryContext breweryContext, 
            IMapper mapper)
        {
            _breweryContext = breweryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandEntity>> GetAllAsync()
            => await _breweryContext.Brands
                        .Select(b => _mapper.Map<BrandEntity>(b))
                        .ToListAsync();

        public async Task<BrandEntity> GetByIdAsync(int id)
        {
            var brandModel = await _breweryContext.Brands.FindAsync(id);

            return _mapper.Map<BrandEntity>(brandModel);
        }

        public async Task<BrandEntity> AddAsync(BrandEntity brandEntity)
        {
            var brandModel = _mapper.Map<BrandModel>(brandEntity);

            await _breweryContext.Brands.AddAsync(brandModel);
            await _breweryContext.SaveChangesAsync();

            return _mapper.Map<BrandEntity>(brandModel);
        }

        public async Task<BrandEntity> UpdateAsync(BrandEntity brandEntity, int id)
        {
            var brandModel = await _breweryContext.Brands.FindAsync(id);

            if (brandModel == null) { return null; }

            _mapper.Map(brandEntity, brandModel);

            _breweryContext.Brands.Attach(brandModel);
            _breweryContext.Brands.Entry(brandModel).State = EntityState.Modified;
            await _breweryContext.SaveChangesAsync();

            return _mapper.Map<BrandEntity>(brandModel);
        }

        public async Task<BrandEntity> DeleteAsync(int id)
        {
            var brandModel = await _breweryContext.Brands.FindAsync(id);

            if (brandModel == null) { return null; }

            _breweryContext.Brands.Remove(brandModel);
            await _breweryContext.SaveChangesAsync();

            return _mapper.Map<BrandEntity>(brandModel);
        }
    }
}
