using _3___Mappers.Dtos.BrandDtos;
using FluentValidation;

namespace _4___API.Validators.BrandValidators
{
    public class BrandInsertValidator : AbstractValidator<BrandInsertDto>
    {
        public BrandInsertValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty()
                    .WithMessage("El campo 'Name' no puede estar vacío.")
                .MaximumLength(30)
                    .WithMessage("El campo 'Name' no puede superar los 30 caracteres.")
                .Matches("^[a-zA-Z0-9áéíóúÁÉÍÓÚàèìòùÀÈÌÒÙäëïöüÄËÏÖÜÑñçÇ]*$").WithMessage("El campo 'Name' solo puede contener caracteres alfanuméricos.");
        }
    }
}
