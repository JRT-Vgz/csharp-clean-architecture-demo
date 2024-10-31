using _3___Mappers.Dtos.BrandDtos;
using FluentValidation;

namespace _4___API.FormValidators.BrandValidators
{
    public class BrandInsertFormValidator : AbstractValidator<BrandInsertDto>
    {
        public BrandInsertFormValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty()
                    .WithMessage("El campo 'Name' es obligatorio.")
                .MaximumLength(30)
                    .WithMessage("El campo 'Name' no puede superar los 30 caracteres.")
                .Matches("^[a-zA-Z0-9áéíóúÁÉÍÓÚàèìòùÀÈÌÒÙäëïöüÄËÏÖÜÑñçÇ]*$").WithMessage("El campo 'Name' solo puede contener caracteres alfanuméricos.");
        }
    }
}
