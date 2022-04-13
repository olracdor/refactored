using System;
using FluentValidation;
using RefactorThis.Dto;

namespace RefactorThis.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is null");
        }
    }
}
