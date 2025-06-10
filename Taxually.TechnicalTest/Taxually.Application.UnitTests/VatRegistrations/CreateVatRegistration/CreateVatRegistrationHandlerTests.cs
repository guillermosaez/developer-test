using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Taxually.Application.VatRegistrations.CreateVatRegistration;

namespace Taxually.Application.UnitTests.VatRegistrations.CreateVatRegistration;

public class CreateVatRegistrationHandlerTests
{
    private readonly Mock<ICreateVatRegistrationClient> _clientMock = new();
    private readonly IServiceProvider _serviceProvider;

    public CreateVatRegistrationHandlerTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddKeyedScoped<ICreateVatRegistrationClient>(Country.GreatBritain, (_, _) => _clientMock.Object);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private CreateVatRegistrationHandler _sut => new(_serviceProvider);

    [Fact]
    public async Task CreateAsync_When_country_doesnt_exist_Then_error_is_returned()
    {
        //Arrange
        var request = new CreateVatRegistrationRequest
        {
            CompanyName = string.Empty,
            CompanyId = string.Empty,
            Country = Country.Undefined
        };
        
        //Act
        var result = await _sut.CreateAsync(request);

        //Assert
        Assert.True(result.IsFailed);
        Assert.Single(result.Errors);
        Assert.IsType<NotSupportedCountryError>(result.Errors[0]);
        _clientMock.Verify(c => c.RegisterAsync(It.IsAny<CreateVatRegistrationRequest>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_When_country_exists_Then_call_to_client_is_done()
    {
        //Arrange
        var request = new CreateVatRegistrationRequest
        {
            CompanyName = "CompanyName",
            CompanyId = "CompanyId",
            Country = Country.GreatBritain
        };
        _clientMock.Setup(c => c.RegisterAsync(request)).ReturnsAsync(Result.Ok);
        
        //Act
        var result = await _sut.CreateAsync(request);

        //Assert
        Assert.True(result.IsSuccess);
    }
}