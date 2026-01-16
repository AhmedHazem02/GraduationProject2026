using FluentValidation;
using G_P2026.Core.Features.Authentications.Commands.Models;

namespace G_P2026.Core.Features.Authentications.Commands.Validations
{
    /// <summary>
    /// Validation rules for Logout command
    /// </summary>
    public class LogoutValidation : AbstractValidator<LogoutModel>
    {
        public LogoutValidation()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");
        }
    }
}
