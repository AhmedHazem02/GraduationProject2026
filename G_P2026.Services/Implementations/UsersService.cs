using G_P2026.Data.DTOs.Auth;
using G_P2026.Infastructure.Context;
using G_P2026.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace G_P2026.Services.Implementations
{
	public class UsersService : IUsersService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IFileService _fileService;

		public UsersService(UserManager<ApplicationUser> userManager, IFileService fileService)
		{
			_userManager = userManager;
			_fileService = fileService;
		}

		public async Task<List<UsersDTO>> GetAllUsersAsync()
		{
			var allUsers = _userManager.Users.ToList();

			if (allUsers == null || allUsers.Count == 0)
			{
				return new List<UsersDTO>();
			}

			var usersList = new List<UsersDTO>();
			foreach (var user in allUsers)
			{
				var roles = await _userManager.GetRolesAsync(user);
				usersList.Add(new UsersDTO
				{
					UserId = user.Id,
					UserName = user.UserName,
					Email = user.Email,
					Status = user.Status,
					Roles = roles.ToList()
				});
			}

			return usersList;
		}

		public async Task<UsersDTO?> GetUserByIdAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
				return null;

			var roles = await _userManager.GetRolesAsync(user);
			return new UsersDTO
			{
				UserId = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				Status = user.Status,
				Roles = roles.ToList()
			};
		}
	 
	}
}
