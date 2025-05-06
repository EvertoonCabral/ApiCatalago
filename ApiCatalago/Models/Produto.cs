using ApiCatalago.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalago.Models;

public class Produto : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = " O nome deve ter entre 3 a 50 caracteres...", MinimumLength =5)]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }
     
    public string? Descricao{ get; set; }
    
    [Required]
    [Column(TypeName ="decimal(10,2)")]
    [Range(1, 100000, ErrorMessage = "O preço do produto deve estar entre {1} e {2}")]
    public decimal? Preco { get; set; } 
    
    public string? ImagemUrl { get; set; }
    
    [Required]
    public float Estoque { get; set; }
    
    public DateTime DataCadastro{ get; set; }
    
    public int? CategoriaId { get; set; }

    [JsonIgnore]
    public virtual Categoria? Categoria { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {

        if(Estoque <= 0)
        {

            yield return new ValidationResult("O estoque deve ser maior que zero",
            new[]
            {    nameof(this.Estoque)}
            );


        }

    }
}
