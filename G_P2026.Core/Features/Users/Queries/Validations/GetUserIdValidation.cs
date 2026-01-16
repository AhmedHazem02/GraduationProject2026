using FluentValidation;
using G_P2026.Core.Features.Users.Queries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_P2026.Core.Features.Users.Queries.Validations
{
	public class GetUserIdValidation: AbstractValidator<GetUserByIdModel>
	{
		public GetUserIdValidation()
		{
			RuleFor(x => x.UserId)
				.NotEmpty().WithMessage("UserId is required");
		}
	}
}
