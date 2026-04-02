namespace Tiktok_Clone.BLL.Pagination
{
    public class PaginationSettings
    {
        private const int _maxPageSize = 20;
        private int _pageSize = 5;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > _maxPageSize ? _maxPageSize : value;
        }
    }
}
