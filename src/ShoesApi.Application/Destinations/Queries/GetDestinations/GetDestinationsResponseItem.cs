namespace ShoesApi.Application.Destinations.Queries.GetDestinations;

/// <summary>
/// ДТО Назначений обуви из <see cref="GetDestinationsResponse"/>
/// </summary>
public class GetDestinationsResponseItem
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
