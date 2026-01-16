namespace G_P2026.Services.Interfaces
{
    /// <summary>
    /// Interface for email service operations
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email asynchronously
        /// </summary>
        /// <param name="toEmail">Recipient email address</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body (HTML supported)</param>
        /// <returns>True if email sent successfully</returns>
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);

        /// <summary>
        /// Sends email confirmation link to user
        /// </summary>
        Task<bool> SendConfirmationEmailAsync(string toEmail, string userId, string token, string baseUrl);

        /// <summary>
        /// Sends password reset link to user
        /// </summary>
        Task<bool> SendPasswordResetEmailAsync(string toEmail, string token, string baseUrl);
    }
}
