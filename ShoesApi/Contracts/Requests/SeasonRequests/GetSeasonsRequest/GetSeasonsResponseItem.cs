namespace ShoesApi.Contracts.Requests.SeasonRequests.GetSeasonsRequest
{
	/// <summary>
	/// Season dto in <see cref="GetSeasonsResponse"/>
	/// </summary>
	public class GetSeasonsResponseItem
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
