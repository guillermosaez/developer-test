using System.Xml.Serialization;
using FluentResults;

namespace Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.Germany;

public class GermanyVatRegistrationClient(ITaxuallyQueueClient queueClient) : ICreateVatRegistrationClient
{
    public async Task<Result> RegisterAsync(CreateVatRegistrationRequest request)
    {
        return await Result.Try(
            () => DoRegisterAsync(request),
            exception => new GermanyVatRegistrationError(exception.Message)
        );
    }
    
    private async Task DoRegisterAsync(CreateVatRegistrationRequest request)
    {
        var serializer = new XmlSerializer(typeof(CreateVatRegistrationRequest));
        await using var stringWriter = new StringWriter();
        serializer.Serialize(stringWriter, request);
        var xml = stringWriter.ToString();
        await queueClient.EnqueueAsync("vat-registration-xml", xml);
    }
}