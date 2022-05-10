namespace ShoesApi.Responses.SeasonResponses.GetSeasonsResponse
{
	public class GetSeasonsResponse
	{
		public int TotalCount { get; set; }
		public List<GetSeasonsResponseItem>? Items { get; set; }
	}
}
