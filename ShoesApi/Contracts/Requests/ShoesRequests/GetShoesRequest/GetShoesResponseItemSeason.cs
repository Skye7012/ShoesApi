namespace ShoesApi.Contracts.Requests.ShoesRequests.GetShoesRequest
{
	/// <summary>
	/// Season dto in <see cref="GetShoesResponseItem"/>
	/// </summary>
	public class GetShoesResponseItemSeason
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
