namespace DigitalWallet.Common.Extensions;

public static class IdentityExtension
{
    public static Guid UserId(this ClaimsPrincipal user)
    {
        try
        {
            if (user.Identity.IsAuthenticated)
            {
                return Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
            else
                return Guid.Empty;
        }
        catch (Exception)
        {
            return Guid.Empty;
        }
    }
}