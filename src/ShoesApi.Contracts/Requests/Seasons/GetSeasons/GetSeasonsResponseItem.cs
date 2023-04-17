namespace ShoesApi.Contracts.Requests.Seasons.GetSeasons;

/// <summary>
/// ДТО Сезона обуви из <see cref="GetSeasonsResponse"/>
/// </summary>
public class GetSeasonsResponseItem
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
