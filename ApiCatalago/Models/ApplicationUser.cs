using Microsoft.AspNetCore.Identity;

namespace ApiCatalago.Models;

public class ApplicationUser : IdentityUser
{

    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExperyTime { get; set; }
    
}