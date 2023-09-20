using FluentValidation;
using Services.Dtos;

namespace Services.Validators
{
    public class NeosPelatisValidator : AbstractValidator<NeosPelatisDto>
    {
        public NeosPelatisValidator()
        {
            RuleFor(x => x.Onoma)
                .NotEmpty().WithMessage("To onoma tou pelati den mporei na einai keno")
                .MaximumLength(100).WithMessage("To onoma tou pelati den mporei na iperbenei tous 100 xaraktires");
            RuleFor(x => x.Tilefono)
                .NotEmpty().WithMessage("To tilefono pelati den mporei na einai keno")
                .MaximumLength(15).WithMessage("To tilefono tou pelati den mporei na iperbenei tous 15 xaraktires");
        }
    }
}
