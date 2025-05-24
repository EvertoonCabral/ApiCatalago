using ApiCatalago.Context;
using ApiCatalago.Models;
using ApiCatalago.Pagination;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        
        public ProdutoRepository(AppDbContext context) : base(context)
        {
            
        }

        public IEnumerable<Produto> GetProdutosPaginado(QueryStringParameters queryStringParameters)
        {
            var produtos = GetAll()
                .OrderBy(p => p.Nome)
                .Skip((queryStringParameters.PageNumber - 1) * queryStringParameters.PageSize)
                .Take(queryStringParameters.PageSize).ToList();

            return produtos;
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria)
        {
            
            var produtos = _context.Produtos.Where(p => p.CategoriaId == idCategoria);
            return produtos;
            
        }

        public PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParams)
        {
            
            var produtos = GetAll().AsQueryable();
            
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
