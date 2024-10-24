
using _2___Services.Interfaces;
using _3___Data;
using _3___Mappers.Dtos.BrandDtos;
using Microsoft.EntityFrameworkCore;

namespace _3___Validators.RequestValidators
{
    public class BrandUpdateValidator : IRequestValidator<BrandUpdateDto>
    {
        public List<string> Errors { get; set; }
        private readonly BreweryContext _breweryContext;
        public BrandUpdateValidator(BreweryContext breweryContext)
        {
            _breweryContext = breweryContext;
            Errors = new List<string>();
        }
        public async Task<bool> Validate(BrandUpdateDto brandUpdateDto)
        {
            var exists = await _breweryContext.Brands.AnyAsync(b => b.Name == brandUpdateDto.Name);

            if (exists)
            {
                Errors.Add("Ya existe una marca con ese nombre.");
                return false;
            }
            return true;
        }
    }
}
