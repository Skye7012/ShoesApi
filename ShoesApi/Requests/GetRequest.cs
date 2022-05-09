namespace ShoesApi.Requests
{
	public class GetRequest
	{
		public int Page { get; set; }
		public int Limit { get; set; }

		public string OrderBy { get; set; } = "Id";

		public bool IsAscending { get; set; }

		//public Func<GetResponseItem, object> GetFilterSelector()
		//{
		//	switch (OrderBy)
		//	{
		//		case "":
		//			return x => x.Id;
		//		case "Id":
		//			return x => x.Id;
		//		case "Price":
		//			return x => x.Price;
		//		case "Name":
		//			return x => x.Name;
		//		default:
		//			throw new Exception("Non valid OrderBy Field");
		//	}
		//	//Func<GetShoesResponseItem, object> IdSelector = x => x.Id;
		//	//Func<GetShoesResponseItem, object> PriceSelector = x => x.Price;
		//	//Func<GetShoesResponseItem, object> BrandSelector = x => x.Name;
		//}
	}
}
