using KonyvKolcsonzoAPI.Models.DTOs;
using KonyvKolcsonzoAPI.Services.IAuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KonyvKolcsonzoAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuth auth;
        public AuthController(IAuth auth) {
            this.auth = auth;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequestDTO registerRequestDTO) {
            var user = await auth.Register(registerRequestDTO);
            if (user != null)
            {
                return StatusCode(201, user);
            }

            return StatusCode(400, user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequestDTO loginRequestDTO) {
            var user = await auth.Login(loginRequestDTO);
            if (user != null)
            {
                return StatusCode(201, user);
            }
            return StatusCode(400, user);
        }

        [HttpPost("assignrole")]
        public async Task<ActionResult> AssignRole(string UserName, string RoleName) {
            var user = await auth.AssignRole(UserName, RoleName);
            if (user != null)
            {
                return StatusCode(201, user);
            }

            return StatusCode(400, user);
        }
    }
}
