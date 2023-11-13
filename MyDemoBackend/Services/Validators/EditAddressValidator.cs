using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Services.Dtos;

namespace Services.Validators
{
    public class EditAddressValidator : AbstractValidator<AddressDto>
    {
        public EditAddressValidator()
        {
            RuleFor(x => x.FullAddress)
                .NotEmpty().WithMessage("Full Address can't be empty")
                .MaximumLength(100).WithMessage("The Full address can't be over 100 characters");
            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("Postal Code can't be empty")
                .NotNull().WithMessage("The PostalCode can't be null")
                .InclusiveBetween(10000, 99999).WithMessage("The PostalCode should be 5 characters long");
            RuleFor(x => x.DoorbellName)
                .NotEmpty().WithMessage("The Doorbell Name can't be empty")
                .MaximumLength(100).WithMessage("The Doorbell Name can't be over 100 characters");
            RuleFor(x => x.Floor)
                .NotEmpty().WithMessage("The Doorbell Name can't be empty")
                .MaximumLength(100).WithMessage("The Doorbell Name can't be over 100 characters");
        }
    }
}
