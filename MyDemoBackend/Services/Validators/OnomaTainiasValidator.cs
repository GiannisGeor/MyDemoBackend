using FluentValidation;
using Services.Dtos;

namespace Services.Validators
{
    public class OnomaTainiasValidator : AbstractValidator<OnomaTainiasDto>
    {
        public OnomaTainiasValidator()
        {
            RuleFor(x => x.Titlos)
                .NotEmpty().WithMessage("O titlos ths tainias den mporei na einai kenos")
                .MaximumLength(100).WithMessage("O titlos den mporei na iperbenei tous 100 xaraktires");
        }
    }
}
