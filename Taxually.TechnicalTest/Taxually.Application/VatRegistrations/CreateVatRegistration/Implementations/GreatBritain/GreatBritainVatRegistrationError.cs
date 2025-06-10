using FluentResults;

namespace Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.GreatBritain;

public class GreatBritainVatRegistrationError : Error
{
    public GreatBritainVatRegistrationError(string message): base($"Great Britain VAT registration failed when calling the client. Reason: {message}") { }
}