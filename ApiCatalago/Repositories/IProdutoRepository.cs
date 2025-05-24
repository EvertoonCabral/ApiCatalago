using ApiCatalago.Models;
using ApiCatalago.Pagination;

namespace ApiCatalago.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {

        IEnumerable<Produto> GetProdutosPaginado(QueryStringParameters queryStringParameters); 
        IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria);
        PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroPreco);
    }
}
