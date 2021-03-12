using FluentValidation;
using MVCStore.Domain.Entities;

namespace MVCStore.Domain.Validations {
    public class ProductValidation : AbstractValidator<Product> {
        public ProductValidation() {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}