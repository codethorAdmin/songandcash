using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Business;

public interface IDocumentService
{
    Task<string> SendDocumentToSign(User user, RecoverableSale recoverableSale, Contract contract);
}
