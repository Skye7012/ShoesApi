namespace ShoesApi.Responses.BrandResponses.GetBrandsResponse
{
	public class GetBrandsResponse
	{
		public int TotalCount { get; set; }
		public List<GetBrandsResponseItem>? Items { get; set; }
	}
}
