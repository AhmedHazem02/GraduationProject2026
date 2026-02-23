using G_P2026.Core.Bases;
using MediatR;
using System.Text.Json.Serialization;

namespace G_P2026.Core.Features.Authentications.Commands.Models
{
    /// <summary>
    /// MediatR command for forgot password - sends reset email
    /// </summary>
    public class ForgotPasswordModel : IRequest<Response<bool>>
    {
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// Base URL for constructing the password reset link (injected from controller, not required in request)
        /// </summary>
        [JsonIgnore]
        public string BaseUrl { get; set; } = string.Empty;
    }
}
