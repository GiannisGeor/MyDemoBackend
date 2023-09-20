using FluentValidation;
using Services.Dtos;

namespace Services.Validators
{
    public class NeaKasetaValidator : AbstractValidator<NeaKasetaDto>
    {
        public NeaKasetaValidator()
        {
            RuleFor(x => x.Tipos)
                .NotEmpty().WithMessage("O tipos ths kasetas den mporei na einai kenos")
                .MaximumLength(100).WithMessage("O tipos den mporei na iperbenei tous 100 xaraktires");
            RuleFor(x => x.Timi)
                .NotEmpty().WithMessage("H timi ths kasetas den mporei na einai keni")
                .NotEqual(0).WithMessage("H timi den mporei na einai 0");
            RuleFor(x => x.Posotita)
                .NotEmpty().WithMessage("H posotita ths kasetas den mporei na einai keni");
            RuleFor(x => x.Tainia).SetValidator(new OnomaTainiasValidator());
        }
    }
}
