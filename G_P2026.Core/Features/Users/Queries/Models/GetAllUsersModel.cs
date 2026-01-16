using G_P2026.Core.Bases;
using G_P2026.Data.DTOs.Auth;
using MediatR;

namespace G_P2026.Core.Features.Users.Queries.Models
{
	public class GetAllUsersModel : IRequest<Response<List<UsersDTO>>>
	{
	}
}
