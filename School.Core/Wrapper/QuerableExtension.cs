using Microsoft.EntityFrameworkCore;

namespace School.Core.Wrapper
{
    public static class QuerableExtension
    {
        public static async Task<PaginationResponse<T>> ToPaginationListAsync<T>(
            this IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            if (source == null) throw new ArgumentNullException("source");

            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;

            int count = await source.AsNoTracking().CountAsync();

            if (count == 0)
                return PaginationResponse<T>.Seccuss(new List<T>(), count, pageNumber, pageSize);

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;

            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return PaginationResponse<T>.Seccuss(items, count, pageNumber, pageSize);
        }
    }
}
