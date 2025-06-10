using FluentResults;

namespace Taxually.Application.VatRegistrations.CreateVatRegistration;

public interface ICreateVatRegistrationClient
{
    Task<Result> RegisterAsync(CreateVatRegistrationRequest request);
}