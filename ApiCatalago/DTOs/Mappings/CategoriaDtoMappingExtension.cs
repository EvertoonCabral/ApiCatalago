using ApiCatalago.Models;

namespace ApiCatalago.DTOs.Mappings;

public static class CategoriaDtoMappingExtension
{
    
    public static Categoria? ToCategoria (this CategoriaDTO categoriaDTO)
    {
        return new Categoria()
        {
            CategoriaId = categoriaDTO.CategoriaId,
            Nome = categoriaDTO.Nome,
            ImagemUrl = categoriaDTO.ImagemUrl
        };
    }

    public static CategoriaDTO? ToCategoriaDto (this Categoria categoria)
    {

        return new CategoriaDTO()
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl
        };
    }

    public static IEnumerable<CategoriaDTO> ToCategoriaDtoList(this IEnumerable<Categoria>? categorias)
    {

        if (categorias is null || !categorias.Any())
        {
            return null;
        }

        return categorias.Select(categoria => new CategoriaDTO
        {

            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl

        }).ToList();

    }
    
    
    
    
}