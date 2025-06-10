namespace Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations;

public interface ITaxuallyQueueClient
{
    Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
}