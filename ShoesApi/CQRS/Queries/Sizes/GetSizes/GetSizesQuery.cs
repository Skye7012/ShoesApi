using MediatR;

namespace ShoesApi.CQRS.Queries.Sizes.GetSizes
{
	/// <summary>
	/// Запрос на получение списка Размеров обуви
	/// </summary>
	public class GetSizesQuery : IRequest<List<int>>
	{
	}
}
