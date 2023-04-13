namespace ShoesApi.Contracts.Requests.Shoes.Common;

/// <summary>
/// ДТО Обуви из <see cref="GetShoesResponse"/>
/// </summary>
public class GetShoesResponseItem
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	public int Id { get; set; }

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
	/// Брэнд обуви
	/// </summary>
	public GetShoesResponseItemBrand Brand { get; set; } = default!;

	/// <summary>
	/// Назначение обуви
	/// </summary>
	public GetShoesResponseItemDestination Destination { get; set; } = default!;

	/// <summary>
	/// Сезон обуви
	/// </summary>
	public GetShoesResponseItemSeason Season { get; set; } = default!;

	/// <summary>
	/// Российские Размеры обуви
	/// </summary>
	public List<int> RuSizes { get; set; } = default!;
}
