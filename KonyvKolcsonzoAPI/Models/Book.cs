using System;
using System.Collections.Generic;

namespace KonyvKolcsonzoAPI.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
