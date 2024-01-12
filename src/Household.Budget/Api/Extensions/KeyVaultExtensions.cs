using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Household.Budget.Api.Extensions;

public static class KeyVaultExtensions
{
    public static void UseKeyVault(this WebApplicationBuilder builder)
    {
        var azureOptions = builder.Configuration.GetSection(AzureOptions.SectionName).Get<AzureOptions>() ?? new();
        if (azureOptions.KeyVault.Enabled)
        {
            var app = azureOptions?.RegisterApp ?? new();
            var credential = new ClientSecretCredential(app.TenantId, app.ClientId, app.ClientSecret);
            var client = new SecretClient(new Uri(azureOptions?.KeyVault.Uri ?? ""), credential);
            builder.Configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
        }
    }
}

public class AzureOptions
{
    public const string SectionName = "Azure";

    public KeyVault KeyVault { get; set; } = new();
    public RegisterApp RegisterApp { get; set; } = new();
}

public class KeyVault
{
    public bool Enabled { get; set; }
    public string Uri { get; set; } = "";
}

public class RegisterApp
{
    public string TenantId { get; set; } = "";
    public string ClientId { get; set; } = "";
    public string ClientSecret { get; set; } = "";
}