namespace School.Core.Wrapper
{
    public class PaginationResponse<T>
    {
        public List<T> Data { get; set; }
        public string NextPageUrl { get; set; }
        public string PrevousPageUrl { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public object Meta { get; set; }
        public bool HasNextPage => CurrentPage < TotalPages;
        public bool HasPrevousPage => CurrentPage > 1;
        public List<string> Messages { get; set; }
        public bool IsSuccess { get; set; }

        private PaginationResponse(
            List<T> data,
            int currentPage,
            int pageSize,
            int totalCount,
            string nextPageUrl,
            string prevousPageUrl
        )
        {
            Data = data;
            CurrentPage = currentPage;
            TotalCount = totalCount;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount / (Double)pageSize);
            NextPageUrl = nextPageUrl;
            PrevousPageUrl = prevousPageUrl;
        }

        public static PaginationResponse<T> Seccuss(List<T> data, int count, int pageNumber, int pageSize)
        {
            string NextPageUrl = "No Next";
            string PrevousUrl = "No Prevous";
            if (pageNumber * pageSize < count)
                NextPageUrl = $"https://localhost:7016/Api/v1/Student/GetAllStudnetsUsingPagination?PageNumber={pageNumber + 1}&PageSize={pageSize}";

            if (pageNumber > 1)
                PrevousUrl = $"https://localhost:7016/Api/v1/Student/GetAllStudnetsUsingPagination?PageNumber={pageNumber - 1}&PageSize={pageSize}";

            return new(data, pageNumber, pageSize, count, NextPageUrl, PrevousUrl);
        }

    }
}
