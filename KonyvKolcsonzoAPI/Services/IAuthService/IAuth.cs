using KonyvKolcsonzoAPI.Models.DTOs;

namespace KonyvKolcsonzoAPI.Services.IAuthService
{
    public interface IAuth
    {
        Task<object> Register(RegisterRequestDTO registerRequestDto);
        Task<object> AssignRole(string UserName, string RoleName);
        Task<object> Login(LoginRequestDTO loginRequest);
    }
}