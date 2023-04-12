using System.Linq.Dynamic.Core;
using ShoesApi.Application.Common.Models;

namespace ShoesApi.Application.Common.Extensions;

/// <summary>
/// Расширение для <see cref="IQueryable"/>
/// </summary>
public static class QueryableExtension
{
	/// <summary>
	/// Сделать пагинацию и сортировку
	/// </summary>
	/// <param name="source">Запрос</param>
	/// <param name="request">Фильтры для запроса</param>
	public static IQueryable<T> Sort<T>(
		this IQueryable<T> source,
		BaseGetQuery request)
	{
		if (source == null)
			throw new ArgumentNullException(nameof(source));

		var query = source.OrderBy(request.OrderBy + (request.IsAscending ? "" : " desc"))
			.Skip((request.Page - 1) * request.Limit);

		if (request.Limit != default)
			query = query.Take(request.Limit);

		return query;
	}
}
