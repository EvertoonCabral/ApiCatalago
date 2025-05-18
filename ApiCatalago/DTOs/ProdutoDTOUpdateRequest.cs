using System.ComponentModel.DataAnnotations;

namespace ApiCatalago.DTOs;

public class ProdutoDTOUpdateRequest : IValidatableObject
{
    [Range(1,999, ErrorMessage = "O estoque deve estar entre {1} e {2}")]
    public float Estoque { get; set; }
    
    public DateTime DataCadastro{ get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {

        if (DataCadastro <= DateTime.Now)
        {
            yield return new ValidationResult("A data deve ser maior que a data atual",
                new[] { nameof(this.DataCadastro) });
        }
        
        
    }
}