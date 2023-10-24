using FluentValidation;
using Services.Dtos;

namespace Services.Validators
{
    public class NewOrderLinesValidator : AbstractValidator<NewOrderLinesDto>
    {
        public NewOrderLinesValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId can't be empty");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity can't be empty")
                .NotNull().WithMessage("Quantity can't be null")
                .GreaterThanOrEqualTo(1).WithMessage("Quantity must be greater than or equal to 1");
            RuleFor(x => x.Comments)
                .MaximumLength(500).WithMessage("The comments can't be over 500 characters");
        }
    }
}
