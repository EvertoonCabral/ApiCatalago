using ApiCatalago.Context;
using ApiCatalago.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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
            
            var produtos = _context.Produtos;

            produtos.Add(produto);
            _context.SaveChanges();
            
            return produto;
        }

        public bool DeleteProduto(int id)
        {
            var produto = _context.Produtos.Find(id);
            
            if (produto is null) return false;
            
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            
            return true;
            
            
        }

        public Produto GetProduto(int id)
        {

            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            
            if (produto is null) return null;
            
            return produto;
            
        }


        public bool UpdateProduto(Produto produto)
        {
            var existing = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == produto.ProdutoId);
            if (existing is null) return false;

            _context.Produtos.Update(produto);
            _context.SaveChanges();
            return true;
            
        }
    }
}
