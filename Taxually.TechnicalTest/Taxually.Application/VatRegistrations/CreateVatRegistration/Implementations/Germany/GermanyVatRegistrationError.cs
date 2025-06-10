using FluentResults;

namespace Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.Germany;

public class GermanyVatRegistrationError : Error
{
    public GermanyVatRegistrationError(string message) : base($"Germany VAT registration failed when calling the client. Reason: {message}") { }
}