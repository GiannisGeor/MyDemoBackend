﻿using FluentValidation;
using Services.Dtos;

namespace Services.Validators
{
    public class NeaTainiaKaiSintelestesValidator : AbstractValidator<NeaTainiaKaiSintelestesDto>
    {
        public NeaTainiaKaiSintelestesValidator()
        {
            RuleFor(x => x.Titlos)
                .NotEmpty().WithMessage("O titlos ths tainias den mporei na einai kenos")
                .MaximumLength(100).WithMessage("O titlos den mporei na iperbenei tous 100 xaraktires");
            RuleFor(x => x.Xronia)
                .Must(HasAValidYear).WithMessage("H xronia prepei na einai metaksi tou 1900 kai 10 xronia meta apo to trexon etos");
            RuleFor(x => x.SintelestesKaiRoloi).NotEmpty().WithMessage("Prepei na exei toulaxiston ena sintelesti");
            RuleForEach(x => x.SintelestesKaiRoloi).SetValidator(new SintelestesKaiRoloiValidator());
        }

        private bool HasAValidYear(int xroniaKikloforias)
        {
            if (xroniaKikloforias < 1900 || xroniaKikloforias > DateTime.Today.Year + 10)
            {
                return false;
            }
            return true;
        }
    }
}
