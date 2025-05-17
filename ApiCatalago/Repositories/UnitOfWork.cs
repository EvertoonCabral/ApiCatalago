using ApiCatalago.Context;

namespace ApiCatalago.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private ICategoriaRepository? _categoriaRepo;
    private IProdutoRepository? _produtoRepo;
    public  AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

//garante que o context somente sera criado caso não exista um.
    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepo ??= new CategoriaRepository(_context);
        }
 
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepo ??= new ProdutoRepository(_context);
        }
    }
    
    
    
    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    
}