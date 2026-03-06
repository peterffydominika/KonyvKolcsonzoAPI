using KonyvKolcsonzoAPI.Models;

namespace KonyvKolcsonzoAPI.Services.IAuthService
{
    public interface ITokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> role);
    }
}