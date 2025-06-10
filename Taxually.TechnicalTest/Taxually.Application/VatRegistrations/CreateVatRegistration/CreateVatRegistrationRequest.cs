namespace Taxually.Application.VatRegistrations.CreateVatRegistration;

public readonly record struct CreateVatRegistrationRequest
{
    public required string CompanyName { get; init; }
    public required string CompanyId { get; init; }
    public required Country Country { get; init; }
}