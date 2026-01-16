using FluentValidation;
using G_P2026.Core.Features.Authentications.Queries.Models;

namespace G_P2026.Core.Features.Authentications.Queries.Validations
{
	public class CheckEmailExistsValidation : AbstractValidator<CheckEmailExistsModel>
	{
		public CheckEmailExistsValidation()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email is required")
				.EmailAddress().WithMessage("Invalid email format");
		}
	}
}
