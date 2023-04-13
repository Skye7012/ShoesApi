namespace ShoesApi.Contracts.Requests.Shoes.PutShoe;

/// <summary>
/// Запрос на обновление обуви
/// </summary>
public class PutShoeRequest
{
	/// <summary>
	/// Наименование
	/// </summary>
	public string Name { get; set; } = default!;

	/// <summary>
	/// Идентификатор файла изображения
	/// </summary>
	public int ImageFileId { get; set; }

	/// <summary>
	/// Цена
	/// </summary>
	public int Price { get; set; }

	/// <summary>
	/// Идентификатор Брэнда обуви
	/// </summary>
	public int BrandId { get; set; }

	/// <summary>
	/// Идентификатор Назначения обуви
	/// </summary>
	public int DestinationId { get; set; }

	/// <summary>
	/// Идентификатор Сезона обуви
	/// </summary>
	public int SeasonId { get; set; }

	/// <summary>
	/// Идентификатор Размеров обуви
	/// </summary>
	public HashSet<int> SizesIds { get; set; } = default!;
}
