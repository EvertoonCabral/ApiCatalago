using ApiCatalago.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalago.Repositories
{
    public interface ICategoriaRepository
    {

      public  IEnumerable<Categoria> GetAllCategorias();

        public Categoria? GetCategoriaById(int id);

        public void AddCategoria(Categoria categoria);

        public void UpdateCategoria(Categoria categoria);

        public void DeleteCategoria(int id);


    }
}
