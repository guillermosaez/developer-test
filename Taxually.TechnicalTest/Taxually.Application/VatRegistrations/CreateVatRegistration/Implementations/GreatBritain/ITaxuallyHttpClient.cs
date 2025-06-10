namespace Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.GreatBritain;

public interface ITaxuallyHttpClient
{
    Task PostAsync<TRequest>(string url, TRequest request);
}