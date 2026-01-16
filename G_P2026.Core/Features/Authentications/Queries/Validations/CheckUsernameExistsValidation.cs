using FluentValidation;
using G_P2026.Core.Features.Authentications.Queries.Models;

namespace G_P2026.Core.Features.Authentications.Queries.Validations
{
	public class CheckUsernameExistsValidation : AbstractValidator<CheckUsernameExistsModel>
	{
		public CheckUsernameExistsValidation()
		{
			RuleFor(x => x.Username)
				.NotEmpty().WithMessage("Username is required");
		}
	}
}
