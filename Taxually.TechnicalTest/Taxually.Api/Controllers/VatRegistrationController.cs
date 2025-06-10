using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Taxually.Application.VatRegistrations.CreateVatRegistration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VatRegistrationController(ICreateVatRegistrationHandler createVatRegistrationHandler) : ControllerBase
{
    /// <summary>
    /// Registers a company for a VAT number in a given country
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateVatRegistrationRequest request)
    {
        var response = await createVatRegistrationHandler.CreateAsync(request);
        return response.ToActionResult();
    }
}