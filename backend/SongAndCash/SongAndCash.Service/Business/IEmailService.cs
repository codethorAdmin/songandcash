using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Business;

public interface IEmailService
{
    Task SendEmailToAdmin(RecoverableSale recoverableSale);
}