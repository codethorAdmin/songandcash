using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using Microsoft.Extensions.Options;
using SongAndCash.Model.Configuration;
using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Business;

public class DocumentService(IOptions<GlobalConfiguration> configuration) : IDocumentService
{
    public async Task<string> SendDocumentToSign(
        User user,
        RecoverableSale recoverableSale,
        Contract contract
    )
    {
        var config = new Configuration(new ApiClient(configuration.Value.Documents.ApiUrl));
        config.AddDefaultHeader("Authorization", configuration.Value.Documents.Token);

        var envelopesApi = new EnvelopesApi(config);
        var envelope = new EnvelopeDefinition
        {
            TemplateId = configuration.Value.Documents.DocumentForContractTemplateId,
            TemplateRoles =
            [
                new TemplateRole
                {
                    Email = "signer@example.com",
                    Name = $"{contract.Name} {contract.LastName}",
                    RoleName = "Signer1",
                    Tabs = new Tabs
                    {
                        TextTabs =
                        [
                            new Text { TabLabel = "Name", Value = contract.Name },
                            new Text { TabLabel = "LastName", Value = contract.LastName },
                            new Text
                            {
                                TabLabel = "DateOfBirth",
                                Value = contract.DateOfBirth.ToString(),
                            },
                            new Text { TabLabel = "FiscalNumber", Value = contract.FiscalNumber },
                            new Text { TabLabel = "IBAN", Value = contract.IBAN },
                            new Text { TabLabel = "Swift", Value = contract.Swift },
                            new Text
                            {
                                TabLabel = "CountryOfResidence",
                                Value = contract.CountryOfResidence,
                            },
                            new Text
                            {
                                TabLabel = "CompleteAddress",
                                Value = contract.CompleteAddress,
                            },
                        ],
                    },
                },
            ],
            Status = "sent",
        };

        EnvelopeSummary result = await envelopesApi.CreateEnvelopeAsync(
            configuration.Value.Documents.AccountId,
            envelope
        );

        return result.EnvelopeId;
    }
}
