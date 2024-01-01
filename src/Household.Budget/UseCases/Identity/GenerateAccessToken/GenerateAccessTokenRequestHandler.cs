using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Household.Budget.Api.Config;
using Household.Budget.Contracts.Constants;
using Household.Budget.Contracts.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Household.Budget;

public interface IGenerateAccessTokenRequestHandler
{
    Task<GenerateAccessTokenResponse> Handle(GenerateAccessTokenRequest request, CancellationToken cancellationToken);
}

public class GenerateAccessTokenRequestHandler : IGenerateAccessTokenRequestHandler
{
  private readonly UserManager<AppIdentityUser> _userManager;
  private readonly JwtSettings _settings;


  public GenerateAccessTokenRequestHandler(UserManager<AppIdentityUser> userManager, IOptionsMonitor<JwtSettings> settings)
  {
    if (settings is null) throw new ArgumentNullException(nameof(settings));

    _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    _settings = settings.CurrentValue;
  }

  public async Task<GenerateAccessTokenResponse> Handle(GenerateAccessTokenRequest request, CancellationToken cancellationToken)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
    {
      Issuer = _settings.Issuer,
      Audience = _settings.Audience,
      Subject = await GetUserClaimsAsync(request),
      Expires = DateTime.UtcNow.AddMinutes(_settings.ExpiresInMinutes),
      SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(_settings.EncodeSecret), SecurityAlgorithms.HmacSha512Signature),
    });

    var encodedToken = tokenHandler.WriteToken(token);
    return new GenerateAccessTokenResponse
    {
      AccessToken = encodedToken,
      ExpiresIn = ((DateTimeOffset)token.ValidTo).ToUnixTimeSeconds(),
    };
  }

  private async Task<ClaimsIdentity> GetUserClaimsAsync(GenerateAccessTokenRequest request)
  {
    var claimsIdentity = new ClaimsIdentity();
    var user = await _userManager.FindByNameAsync(request.UserName);
    if (user is not null)
    {
      var claims = await _userManager.GetClaimsAsync(user) ?? new List<Claim>();
      claims.Add(new Claim(type: IdentityClaims.USER_ID, value: $"{user.Id}"));
      claims.Add(new Claim(type: IdentityClaims.USER_NAME, value: user.UserName));
      claims.Add(new Claim(type: IdentityClaims.USER_EMAIL, value: user.Email));
      claims.Add(new Claim(type: JwtRegisteredClaimNames.Jti, value: $"{Guid.NewGuid()}"));
      claimsIdentity.AddClaims(claims);
    }
    return claimsIdentity;
  }
}