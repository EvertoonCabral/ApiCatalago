using ApiCatalago.Models;
using ApiCatalago.Pagination;

namespace ApiCatalago.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {

      Task <IEnumerable<Produto>> GetProdutosPaginado(QueryStringParameters queryStringParameters); 
        Task<IEnumerable<Produto>>GetProdutosPorCategoria(int idCategoria);
        Task<PagedList<Produto>> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroPreco);
    }
}
