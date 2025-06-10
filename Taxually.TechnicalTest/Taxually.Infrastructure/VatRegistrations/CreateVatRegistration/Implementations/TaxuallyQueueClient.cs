using Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations;

namespace Taxually.Infrastructure.VatRegistrations.CreateVatRegistration.Implementations;

public class TaxuallyQueueClient : ITaxuallyQueueClient
{
    public Task EnqueueAsync<TPayload>(string queueName, TPayload payload)
    {
        // Code to send to message queue removed for brevity
        return Task.CompletedTask;
    }
}