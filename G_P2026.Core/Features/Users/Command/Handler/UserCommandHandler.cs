using G_P2026.Core.Bases;
using G_P2026.Core.Features.Users.Command.Models;
using G_P2026.Data.DTOs.Auth;
using G_P2026.Services.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using G_P2026.Infastructure.Context;


namespace G_P2026.Core.Features.Users.Command.Handler
{
	public class UpdateUserCommandHandler : ResponseHandler,
		IRequestHandler<UpdateUserModel, Response<UpdateUserDto>>,
		IRequestHandler<DeleteUserModel, Response<string>>
	{
		private readonly IUsersService _usersService;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;

		public UpdateUserCommandHandler(IUsersService usersService, UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			_usersService = usersService;
			_userManager = userManager;
			_mapper = mapper;
		}

		public async Task<Response<UpdateUserDto>> Handle(UpdateUserModel request, CancellationToken cancellationToken)
		{
			var existingUser = await _userManager.FindByIdAsync(request.UserId);
			if (existingUser == null)
				return NotFound<UpdateUserDto>("User not found");

			// Map request to existing user
			_mapper.Map(request, existingUser);

			// Update User
			var updatedUser = await _userManager.UpdateAsync(existingUser);

			// Check if update succeeded
			if (!updatedUser.Succeeded)
				return BadRequest<UpdateUserDto>(updatedUser.Errors.Select(e => e.Description).FirstOrDefault() ?? "Failed to update user");

			// Return the updated user
			var updatedUserDTO = _mapper.Map<ApplicationUser, UpdateUserDto>(existingUser);
			return Success(updatedUserDTO);
		}

		public async Task<Response<string>> Handle(DeleteUserModel request, CancellationToken cancellationToken)
		{
			var User=await _userManager.FindByIdAsync(request.UserId);
			if (User == null)return NotFound<string>("User not found");
			var result = await _userManager.DeleteAsync(User);
			if (!result.Succeeded)return BadRequest<string>(result.Errors.Select(e => e.Description).FirstOrDefault() ?? "Failed to delete user");
			return Success($"User deleted successfully With \n ID : {request.UserId} \n Name : {User.UserName}");
		}

	}
}
