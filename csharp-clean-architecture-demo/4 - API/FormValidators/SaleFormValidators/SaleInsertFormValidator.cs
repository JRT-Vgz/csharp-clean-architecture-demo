using _3___Mappers.Dtos.SaleDtos;
using FluentValidation;

namespace _4___API.FormValidators.SaleFormValidators
{
    public class SaleInsertFormValidator : AbstractValidator<SaleInsertDto>
    {
        public SaleInsertFormValidator()
        {
            RuleFor(s => s.Concepts)
            .NotEmpty()
                .WithMessage("El campo 'Concepts' no puede estar vacío.");


            RuleForEach(s => s.Concepts)
            .ChildRules(concept =>
            {
                concept.RuleFor(c => c.IdBeer)
                .GreaterThan(0)
                    .WithMessage("El campo 'IdBeer' debe ser mayor a 0.");

                concept.RuleFor(c => c.Quantity)
                    .GreaterThan(0)
                        .WithMessage("El campo 'Quantity' debe ser mayor a 0.");

                concept.RuleFor(c => c.UnitPrice)
                    .GreaterThan(0)
                        .WithMessage("El campo 'UnitPrice' debe ser mayor a 0.");
            }
                );

        }
    }
}
