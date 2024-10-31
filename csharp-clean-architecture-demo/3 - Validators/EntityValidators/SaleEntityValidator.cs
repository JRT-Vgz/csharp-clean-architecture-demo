using _1___Entities;
using _2___Services._Interfaces;
using _3___Data;
using Microsoft.EntityFrameworkCore;

namespace _3___Validators.EntityValidators
{
    public class SaleEntityValidator : IEntityValidator<SaleEntity>
    {
        public List<string> Errors { get; set; }
        private readonly BreweryContext _breweryContext;
        public SaleEntityValidator(BreweryContext breweryContext)
        {
            Errors = new List<string>();
            _breweryContext = breweryContext;
        }

        public async Task<bool> ValidateAsync(SaleEntity saleEntity)
        {
            if (saleEntity.Concepts.Count == 0) { Errors.Add("Una venta debe tener conceptos.");}

            if (saleEntity.Total <= 0) { Errors.Add("Una venta debe tener más de 0.00€ en Total."); }

            foreach (var concept in saleEntity.Concepts)
            {
                var beerExists = await _breweryContext.Beers.AnyAsync(b => b.Id == concept.IdBeer);
                if (!beerExists) { Errors.Add($"No existe ninguna cerveza con 'Id' {concept.IdBeer}"); }
            }

            if (Errors.Count > 0) { return false; }
            return true;
        }
    }
}
