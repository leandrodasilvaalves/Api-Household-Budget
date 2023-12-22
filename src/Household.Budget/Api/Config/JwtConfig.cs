using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Household.Budget.Api.Config;

public static class JwtConfig
{
    public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtSettings>(config.GetSection(JwtSettings.SectionName));

        var settings = config.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.RequireHttpsMetadata = true;
            opt.SaveToken = true;
            opt.TokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(settings?.EncodeSecret),
                ValidateAudience = true,
                ValidAudience = settings?.Audience,
                ValidateIssuer = true,
                ValidIssuer = settings?.Issuer,
            };
        });
        return services;
    }
}

public class JwtSettings
{
    public const string SectionName = "Jwt";

    public string Secret { get; set; } = "";
    public string? Issuer { get; set; }
    public string Audience { get; set; } = "";
    public int ExpiresInMinutes { get; set; }
    public byte[] EncodeSecret { get => Encoding.ASCII.GetBytes(Secret); }

}
