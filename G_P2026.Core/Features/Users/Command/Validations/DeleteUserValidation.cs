using FluentValidation;
using G_P2026.Core.Features.Users.Command.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_P2026.Core.Features.Users.Command.Validations
{
	public class DeleteUserValidation:AbstractValidator<DeleteUserModel>
	{
		public DeleteUserValidation()
		{
			RuleFor(x => x.UserId)
				.NotEmpty().WithMessage("UserId is required");
		}
	}
}
