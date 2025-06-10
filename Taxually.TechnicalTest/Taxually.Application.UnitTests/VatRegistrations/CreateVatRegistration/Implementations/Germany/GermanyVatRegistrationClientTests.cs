using System.Text;
using System.Xml.Serialization;
using Moq;
using Taxually.Application.VatRegistrations.CreateVatRegistration;
using Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations;
using Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.Germany;

namespace Taxually.Application.UnitTests.VatRegistrations.CreateVatRegistration.Implementations.Germany;

public class GermanyVatRegistrationClientTests
{
    private readonly Mock<ITaxuallyQueueClient> _queueClient = new();

    private GermanyVatRegistrationClient _sut => new(_queueClient.Object);
    
    [Fact]
    public async Task RegisterAsync_When_client_fails_Then_error_is_returned()
    {
        //Arrange
        var request = new CreateVatRegistrationRequest
        {
            CompanyName = "CompanyName",
            CompanyId = "CompanyId",
            Country = Country.Germany
        };
        const string exceptionMessage = "Exception message";
        _queueClient.Setup(c => c.EnqueueAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception(exceptionMessage));
        
        //Act
        var result = await _sut.RegisterAsync(request);

        //Assert
        Assert.True(result.IsFailed);
        Assert.Single(result.Errors);
        Assert.IsType<GermanyVatRegistrationError>(result.Errors[0]);
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
        var serializer = new XmlSerializer(typeof(CreateVatRegistrationRequest));
        await using var stringWriter = new StringWriter();
        serializer.Serialize(stringWriter, request);
        var expectedXml = stringWriter.ToString();
        
        //Act
        var result = await _sut.RegisterAsync(request);
    
        //Assert
        Assert.True(result.IsSuccess);
        _queueClient.Verify(
            c => c.EnqueueAsync("vat-registration-xml", expectedXml),
            Times.Once
        );
    }
}