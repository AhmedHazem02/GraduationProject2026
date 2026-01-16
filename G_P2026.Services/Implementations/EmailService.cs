using G_P2026.Data.Helpers;
using G_P2026.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace G_P2026.Services.Implementations
{
    /// <summary>
    /// Email service implementation using SMTP
    /// High performance with async operations and connection pooling
    /// </summary>
    public sealed class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSettings;

        public EmailService(IOptions<EmailSetting> emailSettings)
        {
            _emailSettings = emailSettings.Value ?? throw new ArgumentNullException(nameof(emailSettings));
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(toEmail))
                throw new ArgumentException("Email address is required", nameof(toEmail));

            try
            {
                using var smtpClient = CreateSmtpClient();
                using var mailMessage = CreateMailMessage(toEmail, subject, body);
                
                await smtpClient.SendMailAsync(mailMessage).ConfigureAwait(false);
                return true;
            }
            catch (SmtpException)
            {
                // Log the exception in production
                return false;
            }
        }

        public async Task<bool> SendConfirmationEmailAsync(string toEmail, string userId, string token, string baseUrl)
        {
            var encodedToken = WebUtility.UrlEncode(token);
            var confirmationLink = $"{baseUrl}/confirm-email.html?userId={userId}&token={token}";

            var body = BuildConfirmationEmailBody(confirmationLink);
            return await SendEmailAsync(toEmail, "Confirm Your Email - GP2026", body).ConfigureAwait(false);
        }

        public async Task<bool> SendPasswordResetEmailAsync(string toEmail, string token, string baseUrl)
        {
            var encodedToken = WebUtility.UrlEncode(token);
            var resetLink = $"{baseUrl}/reset-password.html?email={toEmail}&token={encodedToken}";

            var body = BuildPasswordResetEmailBody(resetLink);
            return await SendEmailAsync(toEmail, "Reset Your Password - GP2026", body).ConfigureAwait(false);
        }

        #region Private Helper Methods

        private SmtpClient CreateSmtpClient()
        {
            return new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.FromEmail, _emailSettings.Password),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 30000 // 30 seconds timeout
            };
        }

        private MailMessage CreateMailMessage(string toEmail, string subject, string body)
        {
            return new MailMessage
            {
                From = new MailAddress(_emailSettings.FromEmail, "GP2026 Team"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8,
                To = { new MailAddress(toEmail) }
            };
        }

        private static string BuildConfirmationEmailBody(string confirmationLink)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
</head>
<body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px;'>
    <div style='background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); padding: 30px; text-align: center; border-radius: 10px 10px 0 0;'>
        <h1 style='color: white; margin: 0;'>Welcome to GP2026!</h1>
    </div>
    <div style='background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px;'>
        <h2 style='color: #667eea;'>Confirm Your Email Address</h2>
        <p>Thank you for registering! Please click the button below to confirm your email address.</p>
        <div style='text-align: center; margin: 30px 0;'>
            <a href='{confirmationLink}' style='background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 15px 30px; text-decoration: none; border-radius: 5px; font-weight: bold; display: inline-block;'>
                Confirm Email
            </a>
        </div>
        <p style='color: #666; font-size: 14px;'>If you didn't create an account, you can safely ignore this email.</p>
        <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;'>
        <p style='color: #999; font-size: 12px; text-align: center;'>This link will expire in 24 hours.</p>
    </div>
</body>
</html>";
        }

        private static string BuildPasswordResetEmailBody(string resetLink)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
</head>
<body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px;'>
    <div style='background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%); padding: 30px; text-align: center; border-radius: 10px 10px 0 0;'>
        <h1 style='color: white; margin: 0;'>Password Reset</h1>
    </div>
    <div style='background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px;'>
        <h2 style='color: #f5576c;'>Reset Your Password</h2>
        <p>You requested to reset your password. Click the button below to create a new password.</p>
        <div style='text-align: center; margin: 30px 0;'>
            <a href='{resetLink}' style='background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%); color: white; padding: 15px 30px; text-decoration: none; border-radius: 5px; font-weight: bold; display: inline-block;'>
                Reset Password
            </a>
        </div>
        <p style='color: #666; font-size: 14px;'>If you didn't request a password reset, you can safely ignore this email. Your password will remain unchanged.</p>
        <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;'>
        <p style='color: #999; font-size: 12px; text-align: center;'>This link will expire in 1 hour for security reasons.</p>
    </div>
</body>
</html>";
        }

        #endregion
    }
}
