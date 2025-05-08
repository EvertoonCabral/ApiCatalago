using ApiCatalago.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalago.Repositories
{
    public interface ICategoriaRepository
    {

        IEnumerable<Categoria> GetAllCategorias();

        ActionResult<Categoria> GetCategoriaById(int id);

        ActionResult<Categoria> AddCategoria(Categoria categoria);

        ActionResult<Categoria> UpdateCategoria(int id, Categoria categoria);

        ActionResult<Categoria> DeleteCategoria(int id);


    }
}
