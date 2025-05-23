namespace ApiCatalago.Pagination;

public abstract class QueryStringParameters
{
    
    const int maxPageSize  = 10;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = maxPageSize;

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