namespace ApiCatalago.DTOs.Authentication;

public class TokenModelDto
{
    public string? AccessToken  { get; set; }
    public string? RefreshToken { get; set; }
}
