using FluentValidation;
using G_P2026.Core.Features.Authentications.Commands.Models;

namespace G_P2026.Core.Features.Authentications.Commands.Validations
{
	public class LoginValidation : AbstractValidator<LoginModel>
	{
		public LoginValidation()
		{
			RuleFor(x => x.EmailOrUsername)
				.NotEmpty().WithMessage("Email or Username is required");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password is required");
		}
	}
}
