using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ei_back.Infrastructure.Context
{
    public class PagedSearchDto<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortDirections { get; set; }
        public List<T> List { get; set; }

        public PagedSearchDto() { }

        public PagedSearchDto(int currentPage, int pageSize, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortDirections = sortDirections;
        }

        public int GetCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }

        public int GetPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}
