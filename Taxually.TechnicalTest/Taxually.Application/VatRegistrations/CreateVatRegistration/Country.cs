using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Taxually.Application.VatRegistrations.CreateVatRegistration;

public enum Country
{
    Undefined,
    [JsonStringEnumMemberName("GB")]
    GreatBritain,
    [JsonStringEnumMemberName("FR")]
    France,
    [JsonStringEnumMemberName("DE")]
    Germany
}