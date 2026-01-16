using FluentValidation;
using G_P2026.Core.Features.Users.Command.Models;

namespace G_P2026.Core.Features.Users.Command.Validations
{
	public class UpdateUserValidation : AbstractValidator<UpdateUserModel>
	{
		public UpdateUserValidation()
		{
			RuleFor(x => x.UserId)
				.NotEmpty().WithMessage("User ID is required");

			RuleFor(x => x.UserName)
				.MinimumLength(3).WithMessage("Username must be at least 3 characters");

			RuleFor(x => x.Email)
				.EmailAddress().WithMessage("Valid email is required");

			RuleFor(x => x.Linkedin)
				.Matches(@"^(https?:\/\/)?(www\.)?linkedin\.com\/.*").WithMessage("Invalid LinkedIn URL")
				.When(x => !string.IsNullOrEmpty(x.Linkedin));
		}
	}
}
