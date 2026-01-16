using G_P2026.Core.Bases;
using G_P2026.Core.Features.Users.Queries.Models;
using G_P2026.Data.DTOs.Auth;
using G_P2026.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_P2026.Core.Features.Users.Queries.Handler
{
	public class UsersQueries : ResponseHandler,
		IRequestHandler<GetAllUsersModel, Response<List<UsersDTO>>>,
		IRequestHandler<GetUserByIdModel, Response<UsersDTO>>
	{
		private readonly IUsersService _Users;
		public UsersQueries(IUsersService users)
		{
			_Users = users;
		}

		public async Task<Response<List<UsersDTO>>> Handle(GetAllUsersModel request, CancellationToken cancellationToken)
		{
			var users = await _Users.GetAllUsersAsync();
			if (users == null || !users.Any())
			{
				return NotFound<List<UsersDTO>>("No users found.");
			}
			return Success(users);
		}

		public async Task<Response<UsersDTO>> Handle(GetUserByIdModel request, CancellationToken cancellationToken)
		{
			var user = await _Users.GetUserByIdAsync(request.UserId);
			if (user == null) return NotFound<UsersDTO>("User not found.");
			return Success(user);
		}
	}
}
