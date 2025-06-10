using System.Text.Json.Serialization;
using Taxually.Application.VatRegistrations.CreateVatRegistration;
using Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations;
using Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.France;
using Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.Germany;
using Taxually.Application.VatRegistrations.CreateVatRegistration.Implementations.GreatBritain;
using Taxually.Infrastructure.VatRegistrations.CreateVatRegistration.Implementations;
using Taxually.Infrastructure.VatRegistrations.CreateVatRegistration.Implementations.GreatBritain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICreateVatRegistrationHandler, CreateVatRegistrationHandler>();
builder.Services.AddKeyedScoped<ICreateVatRegistrationClient, GreatBritainVatRegistrationClient>(Country.GreatBritain);
builder.Services.AddKeyedScoped<ICreateVatRegistrationClient, FranceVatRegistrationClient>(Country.France);
builder.Services.AddKeyedScoped<ICreateVatRegistrationClient, GermanyVatRegistrationClient>(Country.Germany);
builder.Services.AddScoped<ITaxuallyHttpClient, TaxuallyHttpClient>();
builder.Services.AddScoped<ITaxuallyQueueClient, TaxuallyQueueClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

// Make the implicit Program class public so test projects can access it
public partial class Program { }