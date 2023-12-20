using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Household.Budget.Contracts.Models.Identity;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Household.Budget;

public class GenerateAccessTokenRequestHandler : IRequestHandler<GenerateAccessTokenRequest, GenerateAccessTokenResponse>
{
  private readonly UserManager<AppIdentityModel> _userManager;
  private readonly JwtSettings _settings;


  public GenerateAccessTokenRequestHandler(UserManager<AppIdentityModel> userManager, IOptionsMonitor<JwtSettings> settings)
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
          new SymmetricSecurityKey(_settings.EncodeSecret), SecurityAlgorithms.HmacSha256Signature),
    });

    var encodedToken = tokenHandler.WriteToken(token);
    return new GenerateAccessTokenResponse
    {
      AccessToken = encodedToken,
      ExpiresIn = default, //TODO: resolver isso aqui
    };
  }

  private async Task<ClaimsIdentity> GetUserClaimsAsync(GenerateAccessTokenRequest request)
  {
    var claimsIdentity = new ClaimsIdentity();
    var user = await _userManager.FindByNameAsync(request.UserName);
    if (user is not null)
    {
      var claims = await _userManager.GetClaimsAsync(user) ?? new List<Claim>();
      claims.Add(new Claim(type: JwtRegisteredClaimNames.Sub, value: $"{user.Id}"));
      claims.Add(new Claim(type: JwtRegisteredClaimNames.Email, value: user.Email));
      claims.Add(new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()));
      // claims.Add(new Claim(type: JwtRegisteredClaimNames.Nbf, value: DateTime.UtcNow().ToString()));
      // claims.Add(new Claim(type: JwtRegisteredClaimNames.Iat, value: DateTime.UtcNow.ToUnixEpocate().ToString(), ClaimValueTypes.Integer64));
      claimsIdentity.AddClaims(claims);
    }
    return claimsIdentity;
  }
}