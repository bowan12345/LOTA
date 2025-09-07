using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace LOTA.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
           
            System.Console.WriteLine($"Email would be sent to: {email}");
            System.Console.WriteLine($"Subject: {subject}");
            System.Console.WriteLine($"Message: {htmlMessage}");
            
            return Task.CompletedTask;
        }
    }
}
