using System.Linq.Expressions;
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
			return source.OrderBy(request.OrderBy + (request.IsAscending ? "" : " desc"))
				.Skip((request.Page - 1) * request.Limit)
				.Take(request.Limit);
		}

		public static IOrderedQueryable<T> OrderBy<T, TKey>(
			this IQueryable<T> source,
			Expression<Func<T, TKey>> keySelector,
			bool isAscending)
		{
			if (isAscending)
				return source.OrderBy(keySelector);
			else
				return source.OrderByDescending(keySelector);
		}

	}
}
