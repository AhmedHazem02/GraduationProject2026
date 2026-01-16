using FluentValidation;
using G_P2026.Core.Features.Authentications.Commands.Models;

namespace G_P2026.Core.Features.Authentications.Commands.Validations
{
    /// <summary>
    /// Validation rules for ConfirmEmail command
    /// </summary>
    public class ConfirmEmailValidation : AbstractValidator<ConfirmEmailModel>
    {
        public ConfirmEmailValidation()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Confirmation token is required");
        }
    }
}
