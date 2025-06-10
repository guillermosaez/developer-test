using FluentResults;

namespace Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.France;

public class FranceVatRegistrationError : Error
{
    public FranceVatRegistrationError(string message) : base($"France VAT registration failed when calling the client. Reason: {message}") { }
}