using G_P2026.Core.Bases;
using MediatR;

namespace G_P2026.Core.Features.Authentications.Queries.Models
{
	public class CheckUsernameExistsModel : IRequest<Response<bool>>
	{
		public string Username { get; set; }
	}
}
