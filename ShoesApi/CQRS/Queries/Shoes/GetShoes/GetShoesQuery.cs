using MediatR;

namespace ShoesApi.CQRS.Queries.Shoes.GetShoesXml
{
	/// <summary>
	/// Запрос на получение коллекции обуви 
	/// </summary>
	public class GetShoesQuery : BaseGetQuery, IRequest<GetShoesResponse>
	{
		/// <summary>
		/// Фильтр поисковой строки
		/// </summary>
		public string? SearchQuery { get; set; }

		/// <summary>
		/// Фильтр по Брэндам обуви
		/// </summary>
		public List<int>? BrandFilters { get; set; }

		/// <summary>
		/// Фильтр по Назначениям обуви
		/// </summary>
		public List<int>? DestinationFilters { get; set; }

		/// <summary>
		/// Фильтр по Сезонам обуви
		/// </summary>
		public List<int>? SeasonFilters { get; set; }

		/// <summary>
		/// Фильтр по Размерам обуви
		/// </summary>
		public List<int>? SizeFilters { get; set; }
	}
}
