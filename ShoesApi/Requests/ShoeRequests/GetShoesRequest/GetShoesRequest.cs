using ShoesApi.Responses.ShoeResponses.GetShoesResponse;

namespace ShoesApi.Requests.ShoeRequests.GetShoesRequest
{
	public class GetShoesRequest : GetRequest
	{
		public Func<GetShoesResponseItem, object> GetOrderBySelector()
		{
			switch (OrderBy)
			{
				case "":
					return x => x.Id;
				case "Id":
					return x => x.Id;
				case "Price":
					return x => x.Price;
				case "Name":
					return x => x.Name;
				default:
					throw new Exception("Non valid OrderBy Field");
			}
			//Func<GetShoesResponseItem, object> IdSelector = x => x.Id;
			//Func<GetShoesResponseItem, object> PriceSelector = x => x.Price;
			//Func<GetShoesResponseItem, object> BrandSelector = x => x.Name;
		}
	}
}
