namespace ApiCatalogo.Pagination;

public class QueryStringParameters
{
    public int PageNumber { get; set; } = 1;
    public int _pageSize = 10;
    const int maxPageSize = 50;

    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
