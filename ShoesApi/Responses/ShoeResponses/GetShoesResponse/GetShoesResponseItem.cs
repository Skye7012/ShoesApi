namespace ShoesApi.Responses.ShoeResponses.GetShoesResponse
{
	public class GetShoesResponseItem
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string Image { get; set; } = default!;
		public int Price { get; set; }

		public GetShoesResponseItemBrand Brand { get; set; }
		public GetShoesResponseItemDestinaiton Destination { get; set; }
		public GetShoesResponseItemSeason Season { get; set; }

		public List<int> RuSizes { get; set; }
	}
}
