using G_P2026.Core.Bases;
using MediatR;

namespace G_P2026.Core.Features.Authentications.Commands.Models
{
    /// <summary>
    /// MediatR command for email confirmation
    /// </summary>
    public class ConfirmEmailModel : IRequest<Response<bool>>
    {
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
