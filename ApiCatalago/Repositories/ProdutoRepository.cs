using ApiCatalago.Context;
using ApiCatalago.Models;

namespace ApiCatalago.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Produto> GetProdutos()
        {

            var produtos = _context.Produtos;

            if (produtos is null)
            {
                throw new NullReferenceException(nameof(produtos));
            }

            return produtos;

        }

        public Produto CreateProdutos(Produto produto)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduto(int id)
        {
            throw new NotImplementedException();
        }

        public Produto GetProduto(int id)
        {
            throw new NotImplementedException();
        }


        public bool UpdateProduto(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}
