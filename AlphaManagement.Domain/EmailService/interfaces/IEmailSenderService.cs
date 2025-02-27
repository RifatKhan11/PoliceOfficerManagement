using AlphaManagement.DAL.Entity.MasterData;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.EmailService.interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmail(string mailTo,string subject, string message);
        Task SendEmailWithFrom(string mailTo, string name, string subject, string message);
        Task<bool> SaveMailLog(MailLog mailLog); 
    }
}
