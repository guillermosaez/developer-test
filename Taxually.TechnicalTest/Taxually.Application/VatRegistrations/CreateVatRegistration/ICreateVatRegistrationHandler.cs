using FluentResults;

namespace Taxually.Application.VatRegistrations.CreateVatRegistration;

public interface ICreateVatRegistrationHandler
{
    Task<Result> CreateAsync(CreateVatRegistrationRequest request);
}