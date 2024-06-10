namespace DigitalWallet.Features.Account.Common;

public interface ITokenFactoryService
{
    string CreateJwtTokens(User user);
}
public class TokenOption
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenExpirationMinutes { get; set; }
}
public class TokenFactoryService(IOptions<TokenOption> options) : ITokenFactoryService
{
    private readonly IOptions<TokenOption> _options = options;

    public string CreateJwtTokens(User user)
    {
        var claims = new List<Claim>
        {
            // Unique Id for all Jwt tokes
            new(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, StringUtils.CreateCryptographicallySecureGuid(), ClaimValueTypes.String, _options.Value.Issuer),
            // Issuer
            new(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iss, _options.Value.Issuer, ClaimValueTypes.String, _options.Value.Issuer),
            // Issued at
            new(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _options.Value.Issuer),
            // for invalidation
            new(ClaimTypes.SerialNumber, StringUtils.CreateCryptographicallySecureGuid(), ClaimValueTypes.String, _options.Value.Issuer),
            // custom data
            new(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String, _options.Value.Issuer),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var now = DateTime.UtcNow;
        var token = new JwtSecurityToken(
            issuer: _options.Value.Issuer,
            audience: _options.Value.Audience,
            claims: claims,
            notBefore: now,
            expires: now.AddMinutes(_options.Value.AccessTokenExpirationMinutes),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}