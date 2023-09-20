using FluentValidation;
using Services.Dtos;

namespace Services.Validators
{
    public class ProsthikiSintelestonValidator : AbstractValidator<ProsthikiSintelestonDto>
    {
        public ProsthikiSintelestonValidator()
        {
            RuleForEach(x => x.SintelestesKaiRoloi).SetValidator(new SintelestesKaiRoloiValidator());
        }
    }
}
