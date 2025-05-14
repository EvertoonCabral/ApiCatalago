using ApiCatalago.Context;
using ApiCatalago.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        
        public ProdutoRepository(AppDbContext context) : base(context)
        {
            
        }
        public IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria)
        {
            
            var produtos = _context.Produtos.Where(p => p.CategoriaId == idCategoria);
            return produtos;
            
        }
    }
}
