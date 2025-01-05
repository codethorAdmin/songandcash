using SongAndCash.Exceptions;
using SongAndCash.Model.Entity;
using SongAndCash.Repository;
using SongAndCash.Service.Mapper;

namespace SongAndCash.Service.Business;

public class RecoverableSalesService(IUserService userService, 
    IRecoverableSalesService recoverableSalesService,
    IRecoverableSalesMapper recoverableSalesMapper,
    IRecoverableSalesRepository recoverableSalesRepository): IRecoverableSalesService
{
    public async Task<RecoverableSale> GetRecoverableSale(int id)
    {
        var recoverableSale = await recoverableSalesService.GetRecoverableSale(id);
        if (recoverableSale == null) throw new EntityNotFoundException($"Recoverable sale with id {id} was not found.");

        return recoverableSale;
    }

    public async Task<RecoverableSale> CreateRecoverableSale(CreateRecoverableSale createRecoverableSale)
    {
        // TODO: review this user can create for the specified user id (or if auto-calculate the id from JWT Token)
       var user = await userService.GetUser(createRecoverableSale.UserId);
       
       var recoverableSaleToCreate = recoverableSalesMapper.FromCreateToRecoverableSale(createRecoverableSale);
       var createdRecoverableSale = await recoverableSalesRepository.CreateRecoverableSale(recoverableSaleToCreate);
       
       return createdRecoverableSale;
    }
}