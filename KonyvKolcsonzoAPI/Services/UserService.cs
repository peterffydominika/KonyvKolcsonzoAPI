using KonyvKolcsonzoAPI.Models;
using KonyvKolcsonzoAPI.Models.DTOs;
using KonyvKolcsonzoAPI.Service.Interfaces;
using KonyvKolcsonzoAPI.Services.IAuthService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KonyvKolcsonzoAPI.Services {
    public class UserService : IUserService {
        private readonly KonyvkolcsonzoContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenGenerator _tokenGenerator;
        public ResponseDTO responseDto = new();

        public UserService(KonyvkolcsonzoContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenGenerator tokenGenerator) {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<object> LoginAsync(LoginRequestDTO loginRequestDto) {
            try
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(user => user.NormalizedUserName == loginRequestDto.UserName.ToUpper());

                bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (isValid)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = _tokenGenerator.GenerateToken(user, roles);

                    responseDto.Message = "Sikeres hozzárendelés!";
                    responseDto.Result = user;
                    return responseDto;
                }
                responseDto.Message = "Sikertelen hozzárendelés!";
                responseDto.Result = null;
                return responseDto;
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
                responseDto.Result = null;
                return responseDto;
            }
        }

        public async Task<object> RegisterAsync(RegisterRequestDTO registerRequestDto) {
            try
            {
                var user = new ApplicationUser {
                    UserName = registerRequestDto.UserName,
                    Email = registerRequestDto.Email,
                    PasswordHash = _passwordHasher.HashPassword(null, registerRequestDto.Password)
                };
                var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

                if (result.Succeeded)
                {
                    var userReturn = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == registerRequestDto.UserName);

                    responseDto.Message = "User registered successfully!";
                    responseDto.Result = userReturn;
                    return responseDto;
                }
                responseDto.Message = result.Errors.FirstOrDefault().Description;
                responseDto.Result = null;
                return responseDto;
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
                responseDto.Result = ex.HResult;
                return responseDto;
            }
        }
    }
}
