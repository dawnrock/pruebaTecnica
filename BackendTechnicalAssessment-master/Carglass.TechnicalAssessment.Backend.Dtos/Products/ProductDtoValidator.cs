using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.Dtos;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(default(long))
            .WithMessage("El Id del producto es necesario.");

        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage("El nombre del producto es necesario.")
            .MaximumLength(100)
            .WithMessage("El nombre del producto tiene una longitud máxima de 100 caracteres.");
    }
}