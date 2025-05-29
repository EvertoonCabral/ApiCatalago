using ApiCatalago.Context;

namespace ApiCatalago.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private ICategoriaRepository? _categoriaRepo;
    private IProdutoRepository? _produtoRepo;
    private readonly AppDbContext _context; // Modificado para readonly
    private bool _disposed;


    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

//garante que o context somente sera criado caso não exista um.
    public ICategoriaRepository CategoriaRepository
    {
        get { return _categoriaRepo ??= new CategoriaRepository(_context); }
    }

    public IProdutoRepository ProdutoRepository
    {
        get { return _produtoRepo ??= new ProdutoRepository(_context); }
    }


    public void Commit()
    {
        _context.SaveChanges();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose recursos gerenciados
                _context.Dispose();
            }

            // Dispose recursos não gerenciados (se houver)
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Destrutor
    ~UnitOfWork()
    {
        Dispose(false);
    }
}