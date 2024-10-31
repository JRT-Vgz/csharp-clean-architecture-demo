using _1___Entities;
using _2___Services._Interfaces;
using _3___Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace _3___Validators.EntityValidators
{
    public class ConceptEntityValidator : IEntityValidator<ConceptEntity>
    {
        public List<string> Errors { get; set; }
        private readonly BreweryContext _breweryContext;
        public ConceptEntityValidator(BreweryContext breweryContext)
        {
            _breweryContext = breweryContext;
            Errors = new List<string>();
        }

        public async Task<bool> ValidateAsync(ConceptEntity conceptEntity)
        {
            if (conceptEntity.IdSale != 0)
            {
                var saleExists = await _breweryContext.Sales.AnyAsync(s => s.Id == conceptEntity.IdSale);
                if (!saleExists) { Errors.Add($"No existe ninguna venta con 'Id' {conceptEntity.IdSale}"); }
            }

            var beerExists = await _breweryContext.Beers.AnyAsync(b => b.Id == conceptEntity.IdBeer);
            if (!beerExists) { Errors.Add($"No existe ninguna cerveza con 'Id' {conceptEntity.IdBeer}"); }            

            if (Errors.Count > 0) { return false; }
            return true;
        }
    }
}
