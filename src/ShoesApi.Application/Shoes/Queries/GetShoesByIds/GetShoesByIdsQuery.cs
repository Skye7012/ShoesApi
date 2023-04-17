using MediatR;
using ShoesApi.Contracts.Requests.Common;
using ShoesApi.Contracts.Requests.Shoes.Common;

namespace ShoesApi.Application.Shoes.Queries.GetShoesByIds;

/// <summary>
/// Запрос на получение списка обуви по коллекции идентификаторов
/// </summary>
public class GetShoesByIdsQuery : BaseGetRequest, IRequest<GetShoesResponse>
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="ids">Коллекция идентификаторов</param>
	public GetShoesByIdsQuery(int[] ids)
		=> Ids = ids;

	/// <summary>
	/// Коллекция идентификаторов
	/// </summary>
	public int[] Ids { get; set; } = default!;
}
