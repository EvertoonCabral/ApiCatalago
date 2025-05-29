using ApiCatalago.Context;
using ApiCatalago.Models;
using ApiCatalago.Pagination;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        
        public ProdutoRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Produto>> GetProdutosPaginado(QueryStringParameters queryStringParameters)
        {
            var todosOsProdutos = await GetAllAsync();
            
            var produtos =  todosOsProdutos
                .OrderBy(p => p.Nome)
                .Skip((queryStringParameters.PageNumber - 1) * queryStringParameters.PageSize)
                .Take(queryStringParameters.PageSize).ToList();

            return produtos;
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorCategoria(int idCategoria)
        {
            if (_context.Produtos != null)
            {
                var produtos = await _context.Produtos.Where(p => p.CategoriaId == idCategoria).ToListAsync();
                return produtos;
            }
            
            return null!;
        }

        public async Task<PagedList<Produto>> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParams)
        {
            
            var produtos = _context.Set<Produto>().AsQueryable();
            
            if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
            {
                if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
            }
            
            var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtosFiltroParams.PageNumber,
                produtosFiltroParams.PageSize);
            return produtosFiltrados;
            
        }
    }
}
