using ShoesApi.CQRS.Queries;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Extensions
{
	/// <summary>
	/// Расширение для <see cref="IQueryable"/>
	/// </summary>
	public static class QueryableExtension
	{
		/// <summary>
		/// Сделать пагинацию и сортировку
		/// </summary>
		public static IQueryable<T> Sort<T>(
			this IQueryable<T> source,
			BaseGetQuery request)
		{
			var query = source.OrderBy(request.OrderBy + (request.IsAscending ? "" : " desc"))
				.Skip((request.Page - 1) * request.Limit);

			if (request.Limit != default)
				query = query.Take(request.Limit);

			return query;
		}
	}
}
