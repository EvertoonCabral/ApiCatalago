using ApiCatalago.Models;

namespace ApiCatalago.Repositories
{
    public interface IProdutoRepository
    {

        IQueryable<Produto> GetProdutos();
        Produto GetProdutoById(int id);
        Produto CreateProdutos(Produto produto);
        bool UpdateProduto(Produto produto);
        bool DeleteProduto(int id);


    }
}
