using ShoesApi.Contracts.Requests.Common;

namespace ShoesApi.Contracts.Requests.Shoes.GetShoes;

/// <summary>
/// Запрос на получение коллекции обуви 
/// </summary>
public class GetShoesRequest : BaseGetRequest
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
