using System.ComponentModel.DataAnnotations;

namespace G_P2026.Data.DTOs.Auth
{
    /// <summary>
    /// DTO for email confirmation request
    /// </summary>
    public class ConfirmEmailDto
    {
        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; } = string.Empty;
    }
}
