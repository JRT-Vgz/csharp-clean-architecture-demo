using _2___Services.Interfaces;
using _3___Data;
using _3___Mappers.Dtos.BrandDtos;
using Microsoft.EntityFrameworkCore;

namespace _3___Validators.RequestValidators
{
    public class BrandInsertValidator : IRequestValidator<BrandInsertDto>
    {
        public List<string> Errors { get; set; }
        private readonly BreweryContext _breweryContext;
        public BrandInsertValidator(BreweryContext breweryContext)
        {
            _breweryContext = breweryContext;
            Errors = new List<string>();
        }

        public async Task<bool> Validate(BrandInsertDto brandInsertDto)
        {
            var exists = await _breweryContext.Brands.AnyAsync(b => b.Name == brandInsertDto.Name);

            if (exists)
            {
                Errors.Add("Ya existe una marca con ese nombre.");
                return false;
            }
            return true;
        }
    }
}
