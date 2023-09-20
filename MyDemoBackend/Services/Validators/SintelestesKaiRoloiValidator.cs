using FluentValidation;
using Services.Dtos;

namespace Services.Validators
{
    public class SintelestesKaiRoloiValidator : AbstractValidator<SintelestesKaiRoloiDto>
    {
        public SintelestesKaiRoloiValidator()
        {
            RuleFor(x => x.Onoma)
                .NotEmpty().WithMessage("To onoma tou sintelesti den mporei na einai keno")
                .MaximumLength(100).WithMessage("To onoma tou sintelesti den mporei na iperbenei tous 100 xaraktires");
            RuleFor(x => x.Rolos)
                .NotEmpty().WithMessage("O Rolos sintelesti den mporei na einai kenos")
                .MaximumLength(100).WithMessage("To onoma tou sintelesti den mporei na iperbenei tous 100 xaraktires");
        }
    }
}
