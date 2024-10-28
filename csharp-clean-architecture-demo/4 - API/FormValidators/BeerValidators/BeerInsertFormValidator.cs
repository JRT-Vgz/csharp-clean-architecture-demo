using _3___Mappers.Dtos.BeerDtos;
using FluentValidation;

namespace _4___API.FormValidators.BeerValidators
{
    public class BeerInsertFormValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertFormValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty()
                    .WithMessage("El campo 'Name' es obligatorio.")
                .MaximumLength(30)
                    .WithMessage("El campo 'Name' no puede superar los 30 caracteres.")
                .Matches("^[a-zA-Z0-9áéíóúÁÉÍÓÚàèìòùÀÈÌÒÙäëïöüÄËÏÖÜÑñçÇ]*$")
                    .WithMessage("El campo 'Name' solo puede contener caracteres alfanuméricos.");

            RuleFor(b => b.IdBrand)
                .NotEmpty()
                    .WithMessage("El campo 'IdBrand' es obligatorio.")
                .GreaterThan(0)
                    .WithMessage("El valor del campo 'IdBrand' debe ser mayor a 0.");

            RuleFor(b => b.Alcohol)
                .NotEmpty()
                    .WithMessage("El campo 'Alcohol' es obligatorio.")
                .LessThanOrEqualTo(100)
                    .WithMessage("El campo 'Alcohol' no puede superar el valor 100")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("El campo 'Alcohol' no puede ser negativo.");
        }

    }
}
