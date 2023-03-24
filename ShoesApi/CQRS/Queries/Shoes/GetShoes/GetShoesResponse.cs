using ShoesApi.CQRS.Queries.Shoes.GetShoes;

namespace ShoesApi.CQRS.Queries.Shoes.GetShoes
{
	/// <summary>
	/// Ответ на <see cref="GetShoesQuery"/>
	/// </summary>
	public class GetShoesResponse : BaseGetResponse<GetShoesResponseItem>
	{
	}
}
