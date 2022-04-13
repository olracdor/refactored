using FluentValidation;
using RefactorThis.Dto;

namespace RefactorThis.Validators
{
    public class ProductOptionValidator : AbstractValidator<ProductOptionDto>
    {
        public ProductOptionValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is null");
        }
    }
}
