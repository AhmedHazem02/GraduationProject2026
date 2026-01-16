using G_P2026.Core.Bases;
using MediatR;

namespace G_P2026.Core.Features.Authentications.Commands.Models
{
    /// <summary>
    /// MediatR command for resetting password with token
    /// </summary>
    public class ResetPasswordModel : IRequest<Response<bool>>
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
