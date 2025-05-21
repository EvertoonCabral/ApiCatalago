namespace ApiCatalago.Pagination;

public class ProdutosParameters
{
    
    private int maxPageSize { get; set; } = 10;
    public int pageNumber { get; set; } = 1;
    private int _pageSize;

    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value > maxPageSize ? maxPageSize : value;
        }
        
    }
}