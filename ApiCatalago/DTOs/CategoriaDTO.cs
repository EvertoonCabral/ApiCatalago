using System.ComponentModel.DataAnnotations;

namespace ApiCatalago.DTOs;

public class CategoriaDTO
{
 
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(300)]
    public String? ImagemUrl { get; set; }

    [Required]
    [StringLength(55)]
    public String? Nome { get; set; }
    
}