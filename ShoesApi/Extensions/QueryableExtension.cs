using ShoesApi.Requests;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Extensions
{
	public static class QueryableExtension
	{
		public static IQueryable<T> Sort<T>(
			this IQueryable<T> source,
			GetRequest request)
		{
			var query = source.OrderBy(request.OrderBy + (request.IsAscending ? "" : " desc"))
				.Skip((request.Page - 1) * request.Limit);

			if (request.Limit != default)
				query = query.Take(request.Limit);

			return query;
		}
	}
}
