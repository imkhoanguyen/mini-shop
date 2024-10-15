    using Microsoft.EntityFrameworkCore;

    namespace API.Helpers
    {
        public class PageList<T> : List<T>
        {
            public int PageIndex { get; set; }
            public int TotalPages { get; set; }
            public int PageSize { get; set; }
            public int TotalCount { get; set; }
            public bool HasPreviousPage => PageIndex > 1;
            public bool HasNextPage => PageIndex < TotalPages;

            public PageList(List<T> items, int count, int pageIndex, int pageSize)
            {
                PageIndex = pageIndex;
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                PageSize = pageSize;
                TotalCount = count;
                AddRange(items);
            }

            public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, PaginationParams paginationParams)
            {
                var count = await source.CountAsync();
                var items = await source.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                                        .Take(paginationParams.PageSize)
                                        .ToListAsync();
                return new PageList<T>(items, count, paginationParams.PageNumber, paginationParams.PageSize);
            }
        }

    }