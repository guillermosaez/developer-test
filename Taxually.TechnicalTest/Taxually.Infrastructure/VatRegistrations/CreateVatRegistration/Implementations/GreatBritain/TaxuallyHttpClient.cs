using Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.GreatBritain;

namespace Taxually.Infrastructure.VatRegistrations.CreateVatRegistration.Implementations.GreatBritain;

public class TaxuallyHttpClient : ITaxuallyHttpClient
{
    public Task PostAsync<TRequest>(string url, TRequest request)
    {
        // Actual HTTP call removed for purposes of this exercise
        return Task.CompletedTask;
    }
}