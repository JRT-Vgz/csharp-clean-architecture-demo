
using _1___Entities;
using _2___Services._Interfaces;
using _3___Data;
using Microsoft.EntityFrameworkCore;

namespace _3___Validators.EntityValidators
{
    public class BeerEntityValidator : IEntityValidator<BeerEntity>
    {
        public List<string> Errors { get; set; }
        private readonly BreweryContext _breweryContext;
        public BeerEntityValidator(BreweryContext breweryContext)
        {
            Errors = new List<string>();
            _breweryContext = breweryContext;
        }

        public async Task<bool> Validate(BeerEntity beerEntity)
        {
            var beerModel = await _breweryContext.Beers.FirstOrDefaultAsync(b => b.Name == beerEntity.Name);
            if (beerModel != null && beerModel.Id != beerEntity.Id) { Errors.Add("Ya existe una cerveza con ese nombre."); }

            var brandExists = await _breweryContext.Brands.AnyAsync(b => b.Id == beerEntity.IdBrand);
            if (!brandExists) { Errors.Add($"No existe ninguna marca con 'IdBrand' {beerEntity.IdBrand}."); }

            if (Errors.Count > 0) { return false; }
            return true;
        }
    }
}
