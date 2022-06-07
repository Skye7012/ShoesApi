namespace ShoesApi.Contracts.Requests.DestinationRequests.GetDestinationsRequest
{
	/// <summary>
	/// Destination dto in <see cref="GetDestinationsResponse"/>
	/// </summary>
	public class GetDestinationsResponseItem
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
