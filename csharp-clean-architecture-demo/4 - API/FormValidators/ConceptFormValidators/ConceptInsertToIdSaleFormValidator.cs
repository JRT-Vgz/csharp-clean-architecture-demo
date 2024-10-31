using _3___Mappers.Dtos.SaleDtos;
using FluentValidation;

namespace _4___API.FormValidators.ConceptFormValidators
{
    public class ConceptInsertToIdSaleFormValidator : AbstractValidator<ConceptInsertToIdSaleDto>
    {
        public ConceptInsertToIdSaleFormValidator()
        {
            RuleFor(c => c.IdSale)
                .NotEmpty()
                    .WithMessage("El campo 'IdSale' es obligatorio.")
                .GreaterThan(0)
                    .WithMessage("El valor del campo 'IdSale' debe ser mayor a 0.")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("El campo 'IdSale' no puede ser negativo.");

            RuleFor(c => c.IdBeer)
                .NotEmpty()
                    .WithMessage("El campo 'IdBeer' es obligatorio.")
                .GreaterThan(0)
                    .WithMessage("El valor del campo 'IdBeer' debe ser mayor a 0.")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("El campo 'IdBeer' no puede ser negativo.");

            RuleFor(c => c.Quantity)
                .NotEmpty()
                    .WithMessage("El campo 'Quantity' es obligatorio.")
                .GreaterThan(0)
                    .WithMessage("El valor del campo 'Quantity' debe ser mayor a 0.")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("El campo 'Quantity' no puede ser negativo.");

            RuleFor(c => c.UnitPrice)
                .NotEmpty()
                    .WithMessage("El campo 'UnitPrice' es obligatorio.")
                .GreaterThan(0)
                    .WithMessage("El valor del campo 'UnitPrice' debe ser mayor a 0.")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("El campo 'UnitPrice' no puede ser negativo.");
        }
    }
}
