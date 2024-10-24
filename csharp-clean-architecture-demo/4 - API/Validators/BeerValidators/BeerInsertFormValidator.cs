using _3___Mappers.Dtos.BeerDtos;
using FluentValidation;

namespace _4___API.Validators.BeerValidators
{
    public class BeerInsertFormValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertFormValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty()
                    .WithMessage("El campo 'Name' no puede estar vacío.")
                .MaximumLength(30)
                    .WithMessage("El campo 'Name' no puede superar los 30 caracteres.")
                .Matches("^[a-zA-Z0-9áéíóúÁÉÍÓÚàèìòùÀÈÌÒÙäëïöüÄËÏÖÜÑñçÇ]*$")
                    .WithMessage("El campo 'Name' solo puede contener caracteres alfanuméricos.");

            RuleFor(b => b.IdBrand)
                .GreaterThan(0)
                    .WithMessage("El valor del campo 'IdBrand' debe ser mayor a 0.");

            RuleFor(b => b.Alcohol)
                .LessThanOrEqualTo(100)
                    .WithMessage("El campo 'Alcohol' no puede superar el valor 100")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("El campo 'Alcohol' no puede ser negativo.");
        }

    }
}
