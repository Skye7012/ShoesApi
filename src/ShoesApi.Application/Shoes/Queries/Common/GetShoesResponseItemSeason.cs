namespace ShoesApi.Application.Shoes.Queries.Common;

/// <summary>
/// ДТО Сезона обуви из <see cref="GetShoesResponseItem"/>
/// </summary>
public class GetShoesResponseItemSeason
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Наименование
	/// </summary>
	public string Name { get; set; } = default!;
}
