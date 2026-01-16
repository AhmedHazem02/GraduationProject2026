using G_P2026.API.Base;
using G_P2026.Core.Features.Authentications.Queries.Models;
using G_P2026.Core.Features.Users.Command.Models;
using G_P2026.Core.Features.Users.Queries.Models;
using G_P2026.Data.MetaDataApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace G_P2026.API.Controllers
{
	public class UsersController : AppBaseController
	{
		[HttpGet(Router.Users.GetAllUsers)]
		public async Task<IActionResult> GetAllUsers()
		{
			var response = await _mediator.Send(new GetAllUsersModel());
			return NewResult(response);
		}

		[HttpGet(Router.Users.GetUserById)]
		public async Task<IActionResult>GetUserById([FromRoute] string id)
		{
			var response = await _mediator.Send(new GetUserByIdModel{UserId=id });
			return NewResult(response);
		}

		[HttpPut(Router.Users.UpdateUser)]
		public async Task<IActionResult> UpdateUser([FromForm] UpdateUserModel model)
		{
			var response = await _mediator.Send(model);
			return NewResult(response);
		}
		[HttpDelete(Router.Users.DeleteUser)]
		public async Task<IActionResult> DeleteUser([FromRoute] string id)
		{
			var response = await _mediator.Send(new DeleteUserModel { UserId = id });
			return NewResult(response);
		}
	}
}
