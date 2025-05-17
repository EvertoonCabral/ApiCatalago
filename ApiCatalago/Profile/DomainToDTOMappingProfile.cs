using ApiCatalago.DTOs;
using ApiCatalago.Models;

namespace ApiCatalago.Profile;

public class DomainToDTOMappingProfile : AutoMapper.Profile
{

    public DomainToDTOMappingProfile()
    {
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();

    }
    
}