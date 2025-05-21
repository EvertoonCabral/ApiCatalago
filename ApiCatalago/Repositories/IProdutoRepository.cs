using ApiCatalago.Models;
using ApiCatalago.Pagination;

namespace ApiCatalago.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {

        IEnumerable<Produto> GetProdutosPaginado(ProdutosParameters produtosParameters); 
        IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria);
        
    }
}
