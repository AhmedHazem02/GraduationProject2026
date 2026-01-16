using FluentValidation;
using G_P2026.Core.Features.Authentications.Commands.Models;

namespace G_P2026.Core.Features.Authentications.Commands.Validations
{
    /// <summary>
    /// Validation rules for ForgotPassword command
    /// </summary>
    public class ForgotPasswordValidation : AbstractValidator<ForgotPasswordModel>
    {
        public ForgotPasswordValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(256).WithMessage("Email cannot exceed 256 characters");
        }
    }
}
