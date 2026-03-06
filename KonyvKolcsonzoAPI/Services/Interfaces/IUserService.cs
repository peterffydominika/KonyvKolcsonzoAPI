using KonyvKolcsonzoAPI.Models.DTOs;

namespace KonyvKolcsonzoAPI.Service.Interfaces {
    public interface IUserService {
        Task<object> RegisterAsync(RegisterRequestDTO dto);
        Task<object> LoginAsync(LoginRequestDTO dto);
    }
}
