using FluentResults;
using Microsoft.Extensions.DependencyInjection;

namespace Taxually.Application.VatRegistrations.CreateVatRegistration;

public class CreateVatRegistrationHandler(IServiceProvider serviceProvider) : ICreateVatRegistrationHandler
{
    public async Task<Result> CreateAsync(CreateVatRegistrationRequest request)
    {
        var validationResult = ValidateRequest(request);
        if (validationResult.IsFailed)
        {
            return validationResult;
        }

        var client = serviceProvider.GetRequiredKeyedService<ICreateVatRegistrationClient>(request.Country);
        var clientResult = await client.RegisterAsync(request);
        return clientResult;
    }

    private static Result ValidateRequest(CreateVatRegistrationRequest request)
    {
        var validCountries = Enum.GetValues<Country>().Where(c => c != Country.Undefined);
        var existsLanguage = validCountries.Contains(request.Country);
        return Result.OkIf(existsLanguage, new NotSupportedCountryError());
    }
}