namespace ShoesApi.Responses.ShoeResponse
{
    public class ShoeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Image { get; set; } = default!;
        public int? Price { get; set; }

        public ShoeResponseBrand Brand { get; set; }
        public ShoeResponseDestinaiton Destination { get; set; }
        public ShoeResponseSeason Season { get; set; }
    }
}
