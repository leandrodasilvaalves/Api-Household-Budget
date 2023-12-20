using System.Text.Json.Serialization;

namespace Household.Budget;

public class GenerateAccessTokenResponse
{
  [JsonPropertyName("expires_in")]
  public long ExpiresIn { get; set; }

  [JsonPropertyName("access_token")]
  public string AccessToken { get; set; } = "";

  [JsonPropertyName("token_type")]
  public string TokenType { get; set; } = "Bearer";
}