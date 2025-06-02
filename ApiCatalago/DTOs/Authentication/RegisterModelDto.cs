using System.ComponentModel.DataAnnotations;

namespace ApiCatalago.DTOs.Authentication;

public class RegisterModelDto
{
    [Required(ErrorMessage = "User name is required")]
    public string? UserName { get; set; }
    
    [Required(ErrorMessage = "PassWord is required")]
    public string? PassWord { get; set; }
    
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }
}