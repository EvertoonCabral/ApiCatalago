using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalago.Models;

public class Produto
{
    [Key]
    public int ProdutoId { get; set; }

    [Required]
    public string? Nome { get; set; }

    public string? Descricao{ get; set; }
    
    [Required]
    [Column(TypeName ="decimal(10,2)")]
    
    public decimal? Preco { get; set; } 
    
    public string? ImagemUrl { get; set; }
    
    [Required]
    public float Estoque { get; set; }
    
    public DateTime DataCadastro{ get; set; }
    
    public int? CategoriaId { get; set; }

    [JsonIgnore]
    public virtual Categoria? Categoria { get; set; }


}
