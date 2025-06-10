using Moq;
using Taxually.Application.VatRegistrations.CreateVatRegistration;
using Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.GreatBritain;

namespace Taxually.Application.UnitTests.VatRegistrations.CreateVatRegistration.Implementations.GreatBritain;

public class GreatBritainVatRegistrationClientTests
{
    private readonly Mock<ITaxuallyHttpClient> _httpClientMock = new();

    private GreatBritainVatRegistrationClient _sut => new(_httpClientMock.Object);

    [Fact]
    public async Task RegisterAsync_When_client_fails_Then_error_is_returned()
    {
        //Arrange
        var request = new CreateVatRegistrationRequest
        {
            CompanyName = "CompanyName",
            CompanyId = "CompanyId",
            Country = Country.GreatBritain
        };
        const string exceptionMessage = "Exception message";
        _httpClientMock.Setup(c => c.PostAsync(It.IsAny<string>(), It.IsAny<CreateVatRegistrationRequest>())).ThrowsAsync(new Exception(exceptionMessage));
        
        //Act
        var result = await _sut.RegisterAsync(request);

        //Assert
        Assert.True(result.IsFailed);
        Assert.Single(result.Errors);
        Assert.IsType<GreatBritainVatRegistrationError>(result.Errors[0]);
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
        
        //Act
        var result = await _sut.RegisterAsync(request);

        //Assert
        Assert.True(result.IsSuccess);
        _httpClientMock.Verify(c => c.PostAsync("https://api.uktax.gov.uk", request), Times.Once);
    }
}