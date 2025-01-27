using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SongAndCash.Model.Dto;
using SongAndCash.Model.Entity;
using SongAndCash.Service.Business;
using SongAndCash.Service.Mapper;

namespace SongAndCash;

public static class RecoverableSalesEndpoints
{
    public static WebApplication MapRecoverableSalesEndpoints(this WebApplication app)
    {
        app.MapGet(
            "/users/{userId}/recoverablesales",
            async (
                int userId,
                IRecoverableSalesService recoverableSalesService,
                IRecoverableSalesMapper recoverableSalesMapper
            ) =>
            {
                var recoverableSales = await recoverableSalesService.GetRecoverableSales(userId);
                var recoverableSalesDto = recoverableSales.Select(
                    recoverableSalesMapper.MapToRecoverableSaleDto
                );

                return Results.Ok(recoverableSalesDto);
            }
        );

        app.MapGet(
            "/users/{userId}/recoverablesales/{recoverableSaleId}",
            async (
                int userId,
                int recoverableSaleId,
                IRecoverableSalesService recoverableSalesService,
                IRecoverableSalesMapper recoverableSalesMapper
            ) =>
            {
                var recoverableSaleDetails = await recoverableSalesService.GetRecoverableSale(
                    userId,
                    recoverableSaleId
                );
                var recoverableSalesDetailsDto = recoverableSalesMapper.MapToRecoverableSaleDto(
                    recoverableSaleDetails
                );

                return Results.Ok(recoverableSalesDetailsDto);
            }
        );

        app.MapGet(
            "/users/{userId}/recoverablesales/{recoverableSaleId}/lastsixmonthsettlement",
            async (
                int userId,
                int recoverableSaleId,
                IRecoverableSalesService recoverableSalesService
            ) =>
            {
                (byte[] content, string contentType)[] lastSixMonthSettlementFiles =
                    await recoverableSalesService.GetRecoverableSaleLastSixMonthSettlements(
                        userId,
                        recoverableSaleId
                    );

                var files = lastSixMonthSettlementFiles.Select(
                    (x, i) => // create a file from the bytes
                    {
                        var file = new FileContentResult(x.content, x.contentType)
                        {
                            FileDownloadName = $"last-six-month-settlement-{i}.xlsx",
                        };

                        return file;
                    }
                );

                return Results.Ok(files);
            }
        );

        app.MapPost(
            "/users/{userId}/recoverablesales/create",
            async (
                int userId,
                HttpContext context,
                IRecoverableSalesService recoverableSalesService,
                IRecoverableSalesMapper recoverableSalesMapper
            ) =>
            {
                using var reader = new StreamReader(context.Request.Body);
                var bodyText = await reader.ReadToEndAsync();
                var createRecoverableSaleDto = JsonSerializer.Deserialize<CreateRecoverableSaleDto>(
                    bodyText
                );

                if (createRecoverableSaleDto == null)
                {
                    return Results.BadRequest();
                }

                var recoverableSale = await recoverableSalesService.CreateRecoverableSale(
                    recoverableSalesMapper.MapToCreateRecoverableSale(createRecoverableSaleDto)
                );

                return Results.Created(
                    $"/users/{userId}/recoverablesales{recoverableSale.Id}",
                    recoverableSale
                );
            }
        );

        app.MapPost(
            "/users/{userId}/recoverablesales/{recoverableSaleId}/markUnderStudy",
            async (
                int userId,
                int recoverableSaleId,
                IRecoverableSalesService recoverableSalesService,
                IRecoverableSalesMapper recoverableSalesMapper
            ) =>
            {
                var hasBeenMarkedUnderStudy = await recoverableSalesService.MarkUnderStudy(
                    userId,
                    recoverableSaleId
                );

                if (!hasBeenMarkedUnderStudy)
                {
                    return Results.UnprocessableEntity(
                        "The recoverable study could not be marked under study."
                    );
                }

                return Results.Accepted();
            }
        );

        app.MapPost(
            "/users/{userId}/recoverablesales/{recoverableSaleId}/reject",
            async (
                int userId,
                int recoverableSaleId,
                HttpContext context,
                IRecoverableSalesService recoverableSalesService
            ) =>
            {
                var rejectBody = await GetRequestBody<RejectRecoverableSaleDto>(context);

                var hasBeenRejected = await recoverableSalesService.Reject(
                    userId,
                    recoverableSaleId,
                    rejectBody?.Reason
                );

                if (!hasBeenRejected)
                {
                    return Results.UnprocessableEntity(
                        "The recoverable study could not be rejected."
                    );
                }

                return Results.Accepted();
            }
        );

        app.MapPost(
            "/users/{userId}/recoverablesales/{recoverableSaleId}/preaccept",
            async (
                int userId,
                int recoverableSaleId,
                HttpContext context,
                IRecoverableSalesService recoverableSalesService
            ) =>
            {
                var proposal = await GetRequestBody<ProposalDto>(context);
                if (proposal == null)
                {
                    return Results.BadRequest();
                }

                var hasBeenPreAccepted = await recoverableSalesService.PreAcceptByAdmin(
                    userId,
                    recoverableSaleId,
                    new Proposal()
                    {
                        MoneyForArtist = proposal.MoneyForArtist,
                        MoneyToReturn = proposal.MoneyToReturn,
                    }
                );

                return Results.Accepted();
            }
        );

        app.MapPost(
            "/users/{userId}/recoverablesales/{recoverableSaleId}/accept",
            async (
                int userId,
                int recoverableSaleId,
                HttpContext context,
                IRecoverableSalesService recoverableSalesService
            ) =>
            {
                var proposal = await GetRequestBody<ProposalDto>(context);
                if (proposal == null)
                {
                    return Results.BadRequest();
                }

                var isAcceptedByArtist = await recoverableSalesService.AcceptByArtist(
                    userId,
                    recoverableSaleId,
                    new Proposal()
                    {
                        MoneyForArtist = proposal.MoneyForArtist,
                        MoneyToReturn = proposal.MoneyToReturn,
                    }
                );
                if (!isAcceptedByArtist)
                {
                    return Results.UnprocessableEntity(
                        "The recoverable sale could not be accepted by the artist."
                    );
                }

                return Results.Accepted();
            }
        );

        app.MapPost(
            "/users/{userId}/recoverablesales/{recoverableSaleId}/fillcontract",
            async (
                int userId,
                int recoverableSaleId,
                HttpContext context,
                IRecoverableSalesService recoverableSalesService,
                IContractMapper contractMapper
            ) =>
            {
                var contractDetailsDto = await GetRequestBody<ContractDetailsDto>(context);
                if (contractDetailsDto == null)
                {
                    return Results.BadRequest();
                }

                var newContract = await recoverableSalesService.GenerateContract(
                    userId,
                    recoverableSaleId,
                    contractMapper.FromContractDetailsDtoToContractDetails(contractDetailsDto)
                );

                return Results.Created(
                    $"/users/{userId}/recoverablesales/{recoverableSaleId}/contracts/{newContract.Id}",
                    newContract
                );
            }
        );

        return app;
    }

    private static async Task<T?> GetRequestBody<T>(HttpContext context)
    {
        using var reader = new StreamReader(context.Request.Body);
        var bodyText = await reader.ReadToEndAsync();
        if (string.IsNullOrEmpty(bodyText))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(bodyText);
    }
}
