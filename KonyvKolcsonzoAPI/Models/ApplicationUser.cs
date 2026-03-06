using Microsoft.AspNetCore.Identity;

namespace KonyvKolcsonzoAPI.Models {
    public class ApplicationUser : IdentityUser {
        public string? UserName { get; set; }
    }
}
