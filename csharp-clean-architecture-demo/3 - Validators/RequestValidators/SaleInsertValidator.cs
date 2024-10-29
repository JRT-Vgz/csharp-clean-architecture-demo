
using _2___Services.Interfaces;
using _3___Data;
using _3___Mappers.Dtos.SaleDtos;
using Microsoft.EntityFrameworkCore;

namespace _3___Validators.RequestValidators
{
    public class SaleInsertValidator : IRequestValidator<SaleInsertDto>
    {
        public List<string> Errors { get; set; }
        private readonly BreweryContext _breweryContext;

        public SaleInsertValidator(BreweryContext breweryContext) 
        { 
            _breweryContext = breweryContext;
            Errors = new List<string>();
        }
        public async Task<bool> Validate(SaleInsertDto saleInsertDto)
        {
            if (saleInsertDto.Concepts.Count == 0)
            {
                Errors.Add("El campo 'Concepts' no puede estar vacío.");
            }

            foreach (var concept in saleInsertDto.Concepts)
            {
                var beerExists = await _breweryContext.Beers.AnyAsync(b => b.Id == concept.IdBeer);
                if (!beerExists) { Errors.Add($"No existe ninguna cerveza con 'Id' {concept.IdBeer}"); }
            }

            if (Errors.Count > 0) { return false; }
            return true;
        }
    }
}
