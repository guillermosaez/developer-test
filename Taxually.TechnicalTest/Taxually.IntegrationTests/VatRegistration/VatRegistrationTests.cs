using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Taxually.Application.VatRegistrations.CreateVatRegistration;

namespace Taxually.IntegrationTests.VatRegistration;

// These tests only assert the HTTP status code, as the VAT registration clients perform no external interactions.
// If any side effects or integrations are added in the future, those should be asserted here.
public class VatRegistrationTests(WebApplicationFactory<Program> webApplicationFactory) : IClassFixture<WebApplicationFactory<Program>>
{
    private const string BaseUrl = "api/VatRegistration";
    
    [Theory]
    [InlineData(Country.Undefined)]
    [InlineData((Country)999)]
    public async Task CreateVatRegistration_When_country_doesnt_exist_Then_bad_request_is_returned(Country nonSupportedCountry)
    {
        //Arrange
        var request = new CreateVatRegistrationRequest
        {
            CompanyName = "CompanyName",
            CompanyId = "CompanyId",
            Country = nonSupportedCountry
        };
        var httpClient = webApplicationFactory.CreateClient();

        //Act
        var result = await httpClient.PostAsJsonAsync(BaseUrl, request);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
    
    [Theory]
    [InlineData(Country.GreatBritain)]
    [InlineData(Country.France)]
    [InlineData(Country.Germany)]
    public async Task CreateVatRegistration_When_country_is_supported_Then_response_is_ok(Country country)
    {
        //Arrange
        var request = new CreateVatRegistrationRequest
        {
            CompanyName = "CompanyName",
            CompanyId = "CompanyId",
            Country = country
        };
        var httpClient = webApplicationFactory.CreateClient();

        //Act
        var result = await httpClient.PostAsJsonAsync(BaseUrl, request);

        //Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}