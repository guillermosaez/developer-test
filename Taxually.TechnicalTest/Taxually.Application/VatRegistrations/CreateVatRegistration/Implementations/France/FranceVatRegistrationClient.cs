using System.Text;
using FluentResults;

namespace Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.France;

public class FranceVatRegistrationClient(ITaxuallyQueueClient queueClient) : ICreateVatRegistrationClient
{
    public async Task<Result> RegisterAsync(CreateVatRegistrationRequest request)
    {
        return await Result.Try(
            () => DoRegisterAsync(request),
            exception => new FranceVatRegistrationError(exception.Message)
        );
    }

    private Task DoRegisterAsync(CreateVatRegistrationRequest request)
    {
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("CompanyName,CompanyId");
        csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");
        var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
        return queueClient.EnqueueAsync("vat-registration-csv", csv);
    }
}