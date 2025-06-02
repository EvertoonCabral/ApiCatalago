using System.ComponentModel.DataAnnotations;

namespace ApiCatalago.DTOs.Authentication;

public class LoginModelDto
{
    [Required(ErrorMessage = "User name is required")]
    public string? UserName { get; set; }
    
    [Required(ErrorMessage = "PassWord is required")]
    public string? PassWord { get; set; }

    
}