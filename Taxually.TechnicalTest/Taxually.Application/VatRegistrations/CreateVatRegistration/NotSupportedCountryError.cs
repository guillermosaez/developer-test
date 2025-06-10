using FluentResults;

namespace Taxually.Application.VatRegistrations.CreateVatRegistration;

public class NotSupportedCountryError : Error
{
    public NotSupportedCountryError() : base("Country not supported.") { }
}