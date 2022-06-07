namespace ShoesApi.Contracts.Requests.ShoesRequests.GetShoesRequest
{
	/// <summary>
	/// Brand dto in <see cref="GetShoesResponseItem"/>
	/// </summary>
	public class GetShoesResponseItemBrand
	{
		/// <summary>
		/// Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; } = default!;
	}
}
