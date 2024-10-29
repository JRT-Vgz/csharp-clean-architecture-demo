
using _1___Entities;
using _2___Services._Interfaces;
using _2___Services.Exceptions;

namespace _3___Validators.EntityValidators
{
    public class SaleEntityValidator : IEntityValidator<SaleEntity>
    {
        public List<string> Errors { get; set; }
        public SaleEntityValidator()
        {
            Errors = new List<string>();
        }

        public async Task<bool> Validate(SaleEntity saleEntity)
        {
            if (saleEntity.Total <= 0) { Errors.Add("Una venta debe tener más de 0.00€ en Total."); }

            if (Errors.Count > 0) { return false; }
            return true;
        }
    }
}
