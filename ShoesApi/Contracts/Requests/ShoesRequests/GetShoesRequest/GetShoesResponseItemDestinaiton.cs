namespace ShoesApi.Contracts.Requests.ShoesRequests.GetShoesRequest
{
	/// <summary>
	/// Destination dto in <see cref="GetShoesResponseItem"/>
	/// </summary>
	public class GetShoesResponseItemDestination
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
