using System.Security.Claims;

namespace G_P2026.Services.Interfaces
{
	public interface ITokenServices
	{
		string GenerateAccessToken(IEnumerable<Claim> claims);
		string GenerateRefreshToken();
		ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
		int GetAccessTokenExpirationMinutes();
	}
}
