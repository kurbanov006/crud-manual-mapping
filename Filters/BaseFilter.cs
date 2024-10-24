public record BaseFilter
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }

    public BaseFilter()
    {
        PageNumber = 1;
        PageSize = 10;
    }
    public BaseFilter(int pageNumber, int PageSize)
    {
        this.PageNumber = pageNumber <= 0 ? 1 : pageNumber;
        this.PageSize = PageSize <= 0 ? 10 : PageSize;
    }
}