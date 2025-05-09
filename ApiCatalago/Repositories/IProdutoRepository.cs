using ApiCatalago.Models;

namespace ApiCatalago.Repositories
{
    public interface IProdutoRepository
    {

        IQueryable<Produto> GetProdutos();
        Produto GetProduto(int id);
        Produto CreateProdutos(Produto produto);
        bool UpdateProduto(Produto produto);
        bool DeleteProduto(int id);


    }
}
