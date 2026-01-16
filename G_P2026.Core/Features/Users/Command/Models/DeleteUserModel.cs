using G_P2026.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_P2026.Core.Features.Users.Command.Models
{
	public class DeleteUserModel:IRequest<Response<string>>
	{
		public string UserId { get; set; }
	}
}
