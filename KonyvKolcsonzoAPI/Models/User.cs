namespace KonyvKolcsonzoAPI.Models;

public partial class User {
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public string Salt { get; set; }
    public List<Book> Books { get; set; } = new List<Book>();
}
