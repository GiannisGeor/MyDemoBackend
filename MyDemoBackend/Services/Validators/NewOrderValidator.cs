using FluentValidation;
using Services.Dtos;
using Data.Interfaces;
using Services.Interfaces;

namespace Services.Validators
{
    public class NewOrderValidator : AbstractValidator<NewOrderDto>
    {
        private readonly IAddressRepository _addressRepository;
        public NewOrderValidator()
        {
            RuleFor(x => x.OrderComments)
                .MaximumLength(500).WithMessage("The comments can't be over 500 characters");
            RuleFor(x => x.AddressId)
                .NotEmpty().WithMessage("The Address Id can't be empty");
            RuleFor(x => x.StoreId)
                .NotEmpty().WithMessage("StoreId can't be empty");
            RuleFor(x => x.OrderLines)
                .NotEmpty().WithMessage("The OrderLines can't be empty");
            RuleForEach(x => x.OrderLines)
                .SetValidator(new NewOrderLinesValidator());
        }
    }
}
