namespace ApiCatalago.Repositories;

public interface IUnitOfWork
{
    
    ICategoriaRepository CategoriaRepository { get; }
    IProdutoRepository ProdutoRepository { get; }
    void Commit();
    Task CommitAsync();

}