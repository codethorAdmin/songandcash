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
    IEmailService emailService,
    IContractService contractService
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

        await emailService.SendEmailToAdmin(
            createdRecoverableSale,
            "Se ha generado una nueva venta recuperable"
        );

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

        if (hasBeenMarkedUnderStudy)
        {
            await emailService.SendEmailToAdmin(
                recoverableSale,
                "Se ha marcado como estudio una venta recuperable"
            );
        }

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

        var hasBeenRejected = await recoverableSalesRepository.Update(recoverableSale);
        if (hasBeenRejected)
        {
            await emailService.SendEmailToAdmin(
                recoverableSale,
                "Se ha cancelado una venta recuperable"
            );
        }

        return hasBeenRejected;
    }

    public async Task<bool> PreAcceptByAdmin(int userId, int recoverableSaleId, Proposal proposal)
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

        if (recoverableSale.Status == RecoverableSaleStatus.PreAccepted)
        {
            throw new EntityValidationException("The recoverable sale was already pre-accepted.");
        }

        if (recoverableSale.Status == RecoverableSaleStatus.Rejected)
        {
            throw new EntityValidationException(
                "The recoverable sale was rejected and it does not allow any changes."
            );
        }

        if (recoverableSale.Status != RecoverableSaleStatus.UnderStudy)
        {
            throw new EntityValidationException(
                "The recoverable sale is not under study to be pre-accepted."
            );
        }

        recoverableSale.Status = RecoverableSaleStatus.PreAccepted;
        recoverableSale.RejectionReason = string.Empty;
        recoverableSale.FinalPaymentToArtist = proposal.MoneyForArtist;
        recoverableSale.FinalPaymentToReturn = proposal.MoneyToReturn;

        var preAccepted = await recoverableSalesRepository.Update(recoverableSale);
        if (preAccepted)
        {
            await emailService.SendEmailToAdmin(
                recoverableSale,
                "Se ha pre-aceptado una venta recuperable"
            );
        }

        return preAccepted;
    }

    public async Task<bool> AcceptByArtist(int userId, int recoverableSaleId, Proposal proposal)
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

        if (recoverableSale.Status == RecoverableSaleStatus.AcceptedByArtist)
        {
            throw new EntityValidationException("The recoverable sale was already accepted.");
        }

        if (recoverableSale.Status == RecoverableSaleStatus.Rejected)
        {
            throw new EntityValidationException(
                "The recoverable sale was rejected and it does not allow any changes."
            );
        }

        if (recoverableSale.Status != RecoverableSaleStatus.PreAccepted)
        {
            throw new EntityValidationException(
                "The recoverable sale is not yet pre-accepted to be accepted."
            );
        }

        recoverableSale.Status = RecoverableSaleStatus.AcceptedByArtist;
        recoverableSale.RejectionReason = string.Empty;
        recoverableSale.FinalPaymentToArtist = proposal.MoneyForArtist;
        recoverableSale.FinalPaymentToReturn = proposal.MoneyToReturn;

        var accepted = await recoverableSalesRepository.Update(recoverableSale);
        if (accepted)
        {
            await emailService.SendEmailToAdmin(
                recoverableSale,
                "Se ha aceptado una venta recuperable por el artista"
            );
        }
        else
        {
            throw new EntityValidationException("The recoverable sale cannot be accepted.");
        }

        return accepted;
    }

    public async Task<Contract> GenerateContract(
        int userId,
        int recoverableSaleId,
        Contract contract
    )
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

        if (recoverableSale.Status != RecoverableSaleStatus.AcceptedByArtist)
        {
            throw new EntityValidationException(
                "Contract can be created only when proposal is accepted by the artist."
            );
        }

        recoverableSale.Status = RecoverableSaleStatus.ContractGenerated;
        recoverableSale.RejectionReason = string.Empty;
        var updated = await recoverableSalesRepository.Update(recoverableSale);
        if (updated)
        {
            var newContract = await contractService.CreateContract(recoverableSale);
            await emailService.SendEmailToAdmin(
                recoverableSale,
                "Se ha generado un nuevo contrato para el artista"
            );
            return newContract;
        }

        throw new EntityValidationException("Contract cannot be generated.");
    }
}
