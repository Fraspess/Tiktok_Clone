namespace Tiktok_Clone.BLL.Pagination
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; } = [];

        public PaginationMetadata Metadata { get; set; } = new();
    }
}
