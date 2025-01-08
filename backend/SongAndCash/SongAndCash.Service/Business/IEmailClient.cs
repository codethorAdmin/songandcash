using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Business;

public interface IEmailClient
{
    Task SendEmailToAdmin(RecoverableSale recoverableSale, string content);
}
