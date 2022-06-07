using ShoesApi.Contracts;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Extensions
{
	/// <summary>
	/// Extensions for <see cref="IQueryable"/>
	/// </summary>
	public static class QueryableExtension
	{
		/// <summary>
		/// Make pagination and sorting
		/// </summary>
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
