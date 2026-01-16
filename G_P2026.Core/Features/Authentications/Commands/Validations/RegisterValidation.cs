using FluentValidation;
using G_P2026.Core.Features.Authentications.Commands.Models;

namespace G_P2026.Core.Features.Authentications.Commands.Validations
{
	public class RegisterValidation : AbstractValidator<RegisterModel>
	{
		public RegisterValidation()
		{
			RuleFor(x => x.UserName)
				.NotEmpty().WithMessage("Username is required")
				.Length(3, 50).WithMessage("Username must be between 3 and 50 characters");

			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email is required")
				.EmailAddress().WithMessage("Invalid email format");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password is required")
				.MinimumLength(6).WithMessage("Password must be at least 6 characters");

			RuleFor(x => x.ConfirmPassword)
				.NotEmpty().WithMessage("Confirm password is required")
				.Equal(x => x.Password).WithMessage("Password and confirmation password do not match");

			RuleFor(x => x.PhoneNumber)
                .Matches(@"^[0-9+\-\s()]*$").WithMessage("Invalid phone number")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
   
			RuleFor(x => x.Role)
				.NotEmpty().WithMessage("Role is required")
				.Must(role => role == "Student" || role == "Mentor")
				.WithMessage("Role must be either 'Student' or 'Mentor'");
		}
	}
}
