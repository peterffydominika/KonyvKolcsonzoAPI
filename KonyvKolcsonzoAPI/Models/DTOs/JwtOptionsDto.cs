namespace KonyvKolcsonzoAPI.Models.DTOs {
    public class JwtOptionsDto
    {
        public string Secret { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
    }
}