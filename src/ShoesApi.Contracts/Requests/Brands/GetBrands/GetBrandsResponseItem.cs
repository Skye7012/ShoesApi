namespace ShoesApi.Contracts.Requests.Brands.GetBrands;

/// <summary>
/// ДТО Брэндов обуви из <see cref="GetBrandsResponse"/>
/// </summary>
public class GetBrandsResponseItem
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
