using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCatalago.Models;

public class Categoria
{

    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }

    [Key]
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(300)]
    public String? ImagemUrl { get; set; }

    [Required]
    [StringLength(55)]
    public String? Nome { get; set; }

    public virtual ICollection<Produto>? Produtos{ get; set; }

}
