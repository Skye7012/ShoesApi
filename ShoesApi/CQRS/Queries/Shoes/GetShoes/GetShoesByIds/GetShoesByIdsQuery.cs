using MediatR;

namespace ShoesApi.CQRS.Queries.Shoes.GetShoesXml.GetShoesByIds
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
