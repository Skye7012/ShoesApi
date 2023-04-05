using MediatR;
using ShoesApi.CQRS.Queries.Shoes.GetShoes;

namespace ShoesApi.CQRS.Queries.Shoes.GetShoes.GetShoesByIds
{
	/// <summary>
	/// Запрос на получение списка обуви по коллекции идентификаторов
	/// </summary>
	public class GetShoesByIdsQuery : BaseGetQuery, IRequest<GetShoesResponse>
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
}
