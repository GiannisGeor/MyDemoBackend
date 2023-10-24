using FluentValidation;
using Services.Dtos;
using Data.Interfaces;

namespace Services.Validators
{
    public class NewOrderValidator : AbstractValidator<NewOrderDto>
    {
        private readonly IStoreRepository _storeRepository;
        public NewOrderValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The Name can't be empty")
                .MaximumLength(100).WithMessage("The name can't be bigger than 100 characters");
            RuleFor(x => x.OrderComments)
                .MaximumLength(500).WithMessage("The comments can't be over 500 characters");
            RuleFor(x => x.StoreId)
                .NotEmpty().WithMessage("StoreId can't be empty");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("The Address can't be empty");
            RuleFor(x => x.Address.FullAddress)
                .NotEmpty().WithMessage("The Address can't be empty")
                .MaximumLength(100).WithMessage("The Address can't be over 100 characters");
            RuleFor(x => x.Address.PostalCode)
                .NotEmpty().WithMessage("The PostalCode can't be empty")
                .NotNull().WithMessage("The PostalCode can't be null")
                .InclusiveBetween(10000,99999).WithMessage("The PostalCode should be 5 characters long");
            RuleFor(x => x.Address.Phone)
                .NotEmpty().WithMessage("The Phone can't be empty")
                .Length(10).WithMessage("The Phone should be 10 characters long")
                .Must(x => x.All(char.IsDigit)).WithMessage("Phone should contain only digits");
            RuleFor(x => x.OrderLines)
                .NotEmpty().WithMessage("The OrderLines can't be empty");
            RuleForEach(x => x.OrderLines)
                .SetValidator(new NewOrderLinesValidator());
        }
    }
}
