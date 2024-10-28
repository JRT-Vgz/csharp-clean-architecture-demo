using _3___Mappers.Dtos.BrandDtos;
using FluentValidation;

namespace _4___API.FormValidators.BrandValidators
{
    public class BrandUpdateFormValidator : AbstractValidator<BrandUpdateDto>
    {
        public BrandUpdateFormValidator() 
        {
            RuleFor(b => b.Id)
                .NotEmpty()
                    .WithMessage("El campo 'Id' es obligatorio.")
                .GreaterThan(0)
                        .WithMessage("El valor del campo 'Id' debe ser mayor a 0.")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("El campo 'Id' no puede ser negativo.");

            RuleFor(b => b.Name)
                .NotEmpty()
                    .WithMessage("El campo 'Name' es obligatorio.")
                .MaximumLength(30)
                    .WithMessage("El campo 'Name' no puede superar los 30 caracteres.")
                .Matches("^[a-zA-Z0-9]*$").WithMessage("El campo 'Name' solo puede contener caracteres alfanuméricos.");



        }
    }
}
