namespace ShoesApi.Responses.ShoeResponse
{
    public class GetShoesResponse
    {
        public int TotalCount { get; set; }
        public List<GetShoesResponseItem>? Items { get; set; }

    }
}
