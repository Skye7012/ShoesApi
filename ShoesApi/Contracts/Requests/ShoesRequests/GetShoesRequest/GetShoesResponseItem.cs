namespace ShoesApi.Contracts.Requests.ShoesRequests.GetShoesRequest
{
	/// <summary>
	/// Shoe dto in <see cref="GetShoesResponse"/>
	/// </summary>
	public class GetShoesResponseItem
	{
		/// <summary>
		/// id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; } = default!;

		/// <summary>
		/// Image name (for path)
		/// </summary>
		public string Image { get; set; } = default!;

		/// <summary>
		/// Price
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// Brand
		/// </summary>
		public GetShoesResponseItemBrand Brand { get; set; } = default!;

		/// <summary>
		/// Destination
		/// </summary>
		public GetShoesResponseItemDestination Destination { get; set; } = default!;

		/// <summary>
		/// Season
		/// </summary>
		public GetShoesResponseItemSeason Season { get; set; } = default!;

		/// <summary>
		/// RuSizes
		/// </summary>
		public List<int> RuSizes { get; set; } = default!;
	}
}
