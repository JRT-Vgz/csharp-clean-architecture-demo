
using _2___Services.Interfaces;
using _3___Data;
using _3___Mappers.Dtos.BeerDtos;
using Microsoft.EntityFrameworkCore;

namespace _3___Validators.RequestValidators
{
    public class BeerUpdateValidator : IRequestValidator<BeerUpdateDto>
    {
        public List<string> Errors { get; set; }
        private readonly BreweryContext _breweryContext;
        public BeerUpdateValidator(BreweryContext breweryContext)
        {
            _breweryContext = breweryContext;
            Errors = new List<string>();
        }

        public async Task<bool> Validate(BeerUpdateDto beerUpdateDto)
        {
            var beerModel = await _breweryContext.Beers.FirstOrDefaultAsync(b => b.Name == beerUpdateDto.Name);
            if (beerModel != null && beerModel.Id != beerUpdateDto.Id) { Errors.Add("Ya existe una cerveza con ese nombre."); }

            var brandExists = await _breweryContext.Brands.AnyAsync(b => b.Id == beerUpdateDto.IdBrand);
            if (!brandExists) { Errors.Add($"No existe ninguna marca con 'IdBrand' {beerUpdateDto.IdBrand}."); }

            if (Errors.Count > 0) { return false; }
            return true;
        }
    }
}
