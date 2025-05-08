using ApiCatalago.Context;
using ApiCatalago.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> GetAllCategorias()
        {

            var categorias = _context.Categorias.ToList();

            if (categorias == null)
                throw new ArgumentNullException(nameof(categorias));


            return categorias;

        }

        public ActionResult<Categoria> GetCategoriaById(int id)
        {

            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            return categoria;

        }


        public ActionResult<Categoria> AddCategoria(Categoria categoria)
        {

            if (categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return categoria;
        }


        public ActionResult<Categoria> DeleteCategoria(int id)
        {
            var categoria = _context.Categorias.Find(id);

            if (categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));

            }

            _context.Remove(categoria);
            _context.SaveChanges();

            return categoria;

        }

        public ActionResult<Categoria> UpdateCategoria(Categoria categoria)
        {
            if (categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return categoria;


        }

    }

    
}
