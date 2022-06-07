namespace ShoesApi.Contracts.Requests.BrandRequests.GetBrandsRequest
{
	/// <summary>
	/// Brand dto in <see cref="GetBrandsResponse"/>
	/// </summary>
	public class GetBrandsResponseItem
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
