
namespace YourStoreApi.Services
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;//current index

        private int _pageSize = 6;//no in one page
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int? Sale { get; set; }
        public int MinPrice { get; set; }=1;
        public int MaxPrice { get; set; }=1000000;
        public int? Star { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public int? Skip { get; set; }
        public string? Brand { get; set; }
        public int? CategoryId { get; set; }
        public string? SubCategory { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        private string? _search;
        public string? Search
        {
            get => _search;
            set => _search = value?.ToLower();
        }
    }
}