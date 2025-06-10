using System.Text;
using Moq;
using Taxually.Application.VatRegistrations.CreateVatRegistration;
using Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations;
using Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.France;

namespace Taxually.Application.UnitTests.VatRegistrations.CreateVatRegistration.Implementations.France;

public class FranceVatRegistrationClientTests
{
    private readonly Mock<ITaxuallyQueueClient> _queueClient = new();

    private FranceVatRegistrationClient _sut => new(_queueClient.Object);
    
    [Fact]
    public async Task RegisterAsync_When_client_fails_Then_error_is_returned()
    {
        //Arrange
        var request = new CreateVatRegistrationRequest
        {
            CompanyName = "CompanyName",
            CompanyId = "CompanyId",
            Country = Country.France
        };
        const string exceptionMessage = "Exception message";
        _queueClient.Setup(c => c.EnqueueAsync(It.IsAny<string>(), It.IsAny<byte[]>())).ThrowsAsync(new Exception(exceptionMessage));
        
        //Act
        var result = await _sut.RegisterAsync(request);

        //Assert
        Assert.True(result.IsFailed);
        Assert.Single(result.Errors);
        Assert.IsType<FranceVatRegistrationError>(result.Errors[0]);
        Assert.Contains(exceptionMessage, result.Errors[0].Message);
    }
    
    [Fact]
    public async Task RegisterAsync_When_client_doesnt_fail_Then_ok_is_returned()
    {
        //Arrange
        var request = new CreateVatRegistrationRequest
        {
            CompanyName = "CompanyName",
            CompanyId = "CompanyId",
            Country = Country.GreatBritain
        };
        var expectedCsv = $"CompanyName,CompanyId\n{request.CompanyName}{request.CompanyId}\n";
        
        //Act
        var result = await _sut.RegisterAsync(request);
    
        //Assert
        Assert.True(result.IsSuccess);
        _queueClient.Verify(
            c => c.EnqueueAsync("vat-registration-csv", It.Is<byte[]>(byteArray => Encoding.UTF8.GetString(byteArray) == expectedCsv)),
            Times.Once
        );
    }
}