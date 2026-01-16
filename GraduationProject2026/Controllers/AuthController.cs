using G_P2026.API.Base;
using G_P2026.Core.Features.Authentications.Commands.Models;
using G_P2026.Core.Features.Authentications.Queries.Models;
using G_P2026.Data.MetaDataApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace G_P2026.API.Controllers
{
	public class AuthController : AppBaseController
	{
		[HttpPost(Router.Auth.Register)]
		public async Task<IActionResult> Register([FromForm] RegisterModel model)
		{
			// Get base URL for constructing confirmation link
			var baseUrl = $"{Request.Scheme}://{Request.Host}";
			model.BaseUrl = baseUrl;

			var response = await _mediator.Send(model);
			return NewResult(response);
		}

		[HttpPost(Router.Auth.Login)]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			var response = await _mediator.Send(model);
			return NewResult(response);
		}

		[HttpGet(Router.Auth.checkEmailExists)]
		public async Task<IActionResult> CheckEmail([FromRoute] string email)
		{
			var response = await _mediator.Send(new CheckEmailExistsModel { Email = email });
			return NewResult(response);
		}

		[HttpGet(Router.Auth.checkUsernameExists)]
		public async Task<IActionResult> CheckUsername([FromRoute] string username)
		{
			var response = await _mediator.Send(new CheckUsernameExistsModel { Username = username });
			return NewResult(response);
		}

		/// <summary>
		/// Logs out the current user by invalidating their tokens
		/// </summary>
		[Authorize]
		[HttpPost(Router.Auth.Logout)]
		public async Task<IActionResult> Logout()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userId))
				return Unauthorized();

			var response = await _mediator.Send(new LogoutModel { UserId = userId });
			return NewResult(response);
		}

		/// <summary>
		/// Confirms user email using the token sent to their email
		/// </summary>
		[HttpGet(Router.Auth.ConfirmEmail)]
		public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
		{
			if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
				return BadRequest("UserId and Token are required");

			// Decode the token (it was URL-encoded in the email link)
			var decodedToken = System.Net.WebUtility.UrlDecode(token);

			var response = await _mediator.Send(new ConfirmEmailModel 
			{ 
				UserId = userId, 
				Token = decodedToken 
			});
			return NewResult(response);
		}

		/// <summary>
		/// Sends password reset email to the user
		/// </summary>
		[HttpPost(Router.Auth.ForgotPassword)]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
		{
			// Get base URL for constructing reset link
			var baseUrl = $"{Request.Scheme}://{Request.Host}";
			model.BaseUrl = baseUrl;

			var response = await _mediator.Send(model);
			return NewResult(response);
		}

		/// <summary>
		/// Resets user password using the reset token
		/// </summary>
		[HttpPost(Router.Auth.ResetPassword)]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
		{
			var response = await _mediator.Send(model);
			return NewResult(response);
		}
	}
}

