namespace G_P2026.Data.DTOs.Auth
{
    /// <summary>
    /// DTO for logout request
    /// </summary>
    public class LogoutDto
    {
        public string UserId { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
    }
}
