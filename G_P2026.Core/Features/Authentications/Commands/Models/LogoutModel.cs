using G_P2026.Core.Bases;
using MediatR;

namespace G_P2026.Core.Features.Authentications.Commands.Models
{
    /// <summary>
    /// MediatR command for logout operation
    /// </summary>
    public class LogoutModel : IRequest<Response<bool>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
