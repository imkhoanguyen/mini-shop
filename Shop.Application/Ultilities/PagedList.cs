namespace Shop.Application.Ultilities
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        // ex: count = 14 pageSize = 5 => totalPages = 14/5 = 2(default int value) but use double
        // => totalPages = 2.8 use Math.Ceiling(2.8) => totalPages = 3  
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items); // add items to PagedList
        }


    }
}
