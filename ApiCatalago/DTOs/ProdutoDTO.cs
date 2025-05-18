using System.ComponentModel.DataAnnotations;
using ApiCatalago.Validations;

namespace ApiCatalago.DTOs;

public class ProdutoDTO
{
    public int ProdutoId { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = " O nome deve ter entre 3 a 50 caracteres...", MinimumLength =5)]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }
    public string? Descricao{ get; set; }
    [Required]
    [Range(1, 100000, ErrorMessage = "O preço do produto deve estar entre {1} e {2}")]
    public decimal? Preco { get; set; } 
    
    public string? ImagemUrl { get; set; }
    
    public int? CategoriaId { get; set; }


}