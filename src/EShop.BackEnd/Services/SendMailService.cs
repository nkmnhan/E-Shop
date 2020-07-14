using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace EShop.BackEnd.Services
{
    public class SendMailService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
