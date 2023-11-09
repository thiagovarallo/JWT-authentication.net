using Microsoft.AspNetCore.Identity;

namespace JWT_authentication.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
