using SongAndCash.Exceptions;
using SongAndCash.Model.Entity;
using SongAndCash.Model.Enum;
using SongAndCash.Repository;
using SongAndCash.Service.Mapper;

namespace SongAndCash.Service.Business;

public class RecoverableSalesService(
    IUserService userService,
    IRecoverableSalesMapper recoverableSalesMapper,
    IRecoverableSalesRepository recoverableSalesRepository,
    IEmailService emailService
) : IRecoverableSalesService
{
    public async Task<RecoverableSale> GetRecoverableSale(int userId, int recoverableSaleId)
    {
        var recoverableSale = await recoverableSalesRepository.GetRecoverableSale(
            recoverableSaleId
        );
        if (recoverableSale == null || recoverableSale.UserId == userId)
        {
            // If the specified user does not match the owner of the recoverable sale, send not found for security.
            throw new EntityNotFoundException(
                $"Recoverable sale with id {recoverableSaleId} was not found."
            );
        }

        return recoverableSale;
    }

    public async Task<RecoverableSale> CreateRecoverableSale(
        CreateRecoverableSale createRecoverableSale
    )
    {
        // TODO: review this user can create for the specified user id (or if auto-calculate the id from JWT Token)
        var user = await userService.GetUser(createRecoverableSale.UserId);
        if (user == null)
        {
            throw new EntityValidationException("Invalid user");
        }

        var recoverableSaleToCreate = recoverableSalesMapper.FromCreateToRecoverableSale(
            createRecoverableSale,
            user.Id
        );
        var createdRecoverableSale = await recoverableSalesRepository.CreateRecoverableSale(
            recoverableSaleToCreate
        );

        await emailService.SendEmailToAdmin(createdRecoverableSale);

        return createdRecoverableSale;
    }

    public async Task<List<RecoverableSale>> GetRecoverableSales(int userId)
    {
        var recoverableSales = await recoverableSalesRepository.GetRecoverableSales(userId);
        return recoverableSales;
    }

    public async Task<bool> MarkUnderStudy(int userId, int recoverableSaleId)
    {
        // TODO: review this user can create for the specified user id (or if auto-calculate the id from JWT Token)
        var user = await userService.GetUser(userId);
        if (user == null)
        {
            throw new EntityValidationException("Invalid user");
        }

        var recoverableSale = await recoverableSalesRepository.GetRecoverableSale(
            recoverableSaleId
        );
        if (user.Id != recoverableSale.UserId)
        {
            throw new EntityValidationException("Invalid user");
        }

        if (recoverableSale.Status == RecoverableSaleStatus.UnderStudy)
        {
            throw new EntityValidationException("The recoverable sale is already under study.");
        }

        recoverableSale.Status = RecoverableSaleStatus.UnderStudy;
        var hasBeenMarkedUnderStudy = await recoverableSalesRepository.Update(recoverableSale);
        return hasBeenMarkedUnderStudy;
    }

    public async Task<bool> Reject(int userId, int recoverableSaleId, string? rejectBodyReason)
    {
        // TODO: review this user can create for the specified user id (or if auto-calculate the id from JWT Token)
        var user = await userService.GetUser(userId);
        if (user == null)
        {
            throw new EntityValidationException("Invalid user");
        }

        var recoverableSale = await recoverableSalesRepository.GetRecoverableSale(
            recoverableSaleId
        );
        if (user.Id != recoverableSale.UserId)
        {
            throw new EntityValidationException("Invalid user");
        }

        if (recoverableSale.Status == RecoverableSaleStatus.Rejected)
        {
            throw new EntityValidationException("The recoverable sale was already rejected.");
        }

        recoverableSale.Status = RecoverableSaleStatus.Rejected;
        recoverableSale.RejectionReason = rejectBodyReason;

        var hasBeenMarkedUnderStudy = await recoverableSalesRepository.Update(recoverableSale);
        return hasBeenMarkedUnderStudy;
    }
}
