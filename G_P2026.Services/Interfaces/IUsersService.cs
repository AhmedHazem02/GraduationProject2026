using G_P2026.Data.DTOs.Auth;
using G_P2026.Infastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_P2026.Services.Interfaces
{
	public interface IUsersService
	{
		Task<List<UsersDTO>> GetAllUsersAsync();
		Task<UsersDTO?> GetUserByIdAsync(string userId);
	 
	}
}
