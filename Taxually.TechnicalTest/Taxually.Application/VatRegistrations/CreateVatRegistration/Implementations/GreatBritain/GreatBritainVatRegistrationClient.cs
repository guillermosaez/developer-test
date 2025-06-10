using FluentResults;

namespace Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.GreatBritain;

public class GreatBritainVatRegistrationClient(ITaxuallyHttpClient httpClient) : ICreateVatRegistrationClient
{
    public async Task<Result> RegisterAsync(CreateVatRegistrationRequest request)
    {
        return await Result.Try(
            () => httpClient.PostAsync("https://api.uktax.gov.uk", request),
            exception => new GreatBritainVatRegistrationError(exception.Message)
        );
    }
}