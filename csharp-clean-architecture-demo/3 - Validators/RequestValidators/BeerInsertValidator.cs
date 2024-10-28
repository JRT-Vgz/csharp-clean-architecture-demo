
using _2___Services.Interfaces;
using _3___Data;
using _3___Mappers.Dtos.BeerDtos;
using Microsoft.EntityFrameworkCore;

namespace _3___Validators.RequestValidators
{
    public class BeerInsertValidator : IRequestValidator<BeerInsertDto>
    {
        public List<string> Errors { get; set; }
        private readonly BreweryContext _breweryContext;
        public BeerInsertValidator(BreweryContext breweryContext)
        {  
            _breweryContext = breweryContext;
            Errors = new List<string>();
        }

        public async Task<bool> Validate(BeerInsertDto beerInsertDto)
        {
            var nameExists = await _breweryContext.Beers.AnyAsync(b => b.Name == beerInsertDto.Name);
            if (nameExists) { Errors.Add("Ya existe una cerveza con ese nombre."); }

            var brandExists = await _breweryContext.Brands.AnyAsync(b => b.Id == beerInsertDto.IdBrand);
            if (!brandExists) { Errors.Add($"No existe ninguna marca con 'IdBrand' {beerInsertDto.IdBrand}."); }

            if (Errors.Count > 0) { return false; }
            return true;
        }
    }
}
