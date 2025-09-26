using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using LOTA.Model;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace LOTA.Utility
{
    public class EmailSender : IEmailSender, ILOTAEmailSender
    {
        private readonly SmtpOptions _smtpOptions;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmailSender(IOptions<SmtpOptions> smtpOptions, IHttpContextAccessor httpContextAccessor)
        {
            _smtpOptions = smtpOptions.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                // If SMTP is not configured, fall back to console output
                if (string.IsNullOrEmpty(_smtpOptions.Host) || string.IsNullOrEmpty(_smtpOptions.UserName))
                {
                    System.Console.WriteLine($"SMTP not configured. Email would be sent to: {email}");
                    System.Console.WriteLine($"Subject: {subject}");
                    System.Console.WriteLine($"Message: {htmlMessage}");
                    return;
                }

                using var client = new SmtpClient(_smtpOptions.Host, _smtpOptions.Port);
                client.Credentials = new NetworkCredential(_smtpOptions.UserName, _smtpOptions.Password);
                client.EnableSsl = _smtpOptions.EnableSsl;

                var fromAddress = string.IsNullOrEmpty(_smtpOptions.From) 
                    ? _smtpOptions.UserName 
                    : _smtpOptions.From;

                var fromName = string.IsNullOrEmpty(_smtpOptions.DisplayName) 
                    ? "LOTA System" 
                    : _smtpOptions.DisplayName;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromAddress, fromName),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
                System.Console.WriteLine($"Email sent successfully to: {email}");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Failed to send email to {email}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Send account creation email to user with login credentials
        /// </summary>
        /// <param name="user">User account</param>
        /// <param name="password">Generated password</param>
        /// <param name="userType">User type (Student/Tutor)</param>
        /// <param name="loginUrl">Login page URL</param>
        public async Task SendAccountCreationEmailAsync(ApplicationUser user, string password, string userType, string loginUrl = null)
        {
            try
            {
                // Generate dynamic login URL if not provided
                if (string.IsNullOrEmpty(loginUrl))
                {
                    var httpContext = _httpContextAccessor.HttpContext;
                    if (httpContext != null)
                    {
                        var scheme = httpContext.Request.Scheme;
                        var host = httpContext.Request.Host;
                        loginUrl = $"{scheme}://{host}/Identity/Account/Login";
                    }
                    else
                    {
                        // Fallback if HttpContext is not available
                        loginUrl = "/Identity/Account/Login";
                    }
                }

                var subject = $"Welcome to LOTA - Your {userType} Account Has Been Created";
                var htmlMessage = $@"
                    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                        <h2 style='color: #2c3e50;'>Welcome to LOTA!</h2>
                        <p>Dear {user.FirstName} {user.LastName},</p>
                        <p>Your {userType.ToLower()} account has been successfully created in the LOTA system.</p>
                        
                        <div style='background-color: #f8f9fa; padding: 20px; border-radius: 5px; margin: 20px 0;'>
                            <h3 style='color: #495057; margin-top: 0;'>Your Login Credentials:</h3>
                            <p><strong>Email:</strong> {user.Email}</p>
                            <p><strong>Password:</strong> {password}</p>
                        </div>
                        
                        <div style='background-color: #fff3cd; padding: 15px; border-radius: 5px; margin: 20px 0;'>
                            <p style='margin: 0; color: #856404;'><strong>Important:</strong> You must change your password on your first login for security reasons.</p>
                        </div>
                        
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='{loginUrl}' style='background-color: #198754; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; font-weight: bold; display: inline-block;'>Login to LOTA</a>
                        </div>
                        
                        <p>If you have any questions or need assistance, please contact the system administrator.</p>
                        
                        <hr style='margin: 30px 0; border: none; border-top: 1px solid #dee2e6;'>
                        <p style='color: #6c757d; font-size: 12px;'>This is an automated message. Please do not reply to this email.</p>
                    </div>";

                await SendEmailAsync(user.Email, subject, htmlMessage);
            }
            catch (System.Exception ex)
            {
                // Log the error but don't fail the user creation
                System.Console.WriteLine($"Failed to send account creation email to {user.Email}: {ex.Message}");
            }
        }
    }
}
