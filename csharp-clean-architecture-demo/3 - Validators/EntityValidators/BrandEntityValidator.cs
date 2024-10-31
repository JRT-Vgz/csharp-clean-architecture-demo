using _1___Entities;
using _2___Services._Interfaces;
using _3___Data;
using Microsoft.EntityFrameworkCore;

namespace _3___Validators.EntityValidators
{
    public class BrandEntityValidator : IEntityValidator<BrandEntity>
    {
        public List<string> Errors { get; set; }
        private readonly BreweryContext _breweryContext;
        public BrandEntityValidator(BreweryContext breweryContext)
        {
            Errors = new List<string>();
            _breweryContext = breweryContext;
        }

        public async Task<bool> ValidateAsync(BrandEntity brandEntity)
        {
            var brandModel = await _breweryContext.Brands.FirstOrDefaultAsync(b => b.Name == brandEntity.Name);
            if (brandModel != null && brandModel.Id != brandEntity.Id) { Errors.Add("Ya existe una marca con ese nombre."); }

            if (Errors.Count > 0) { return false; }
            return true;
        }
    }
}
