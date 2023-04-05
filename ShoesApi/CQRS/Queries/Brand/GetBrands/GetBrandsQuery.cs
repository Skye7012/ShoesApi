using MediatR;

namespace ShoesApi.CQRS.Queries.Brand.GetBrands
{
	/// <summary>
	/// Запрос на получение коллекции Брэндов обуви
	/// </summary>
	public class GetBrandsQuery : IRequest<GetBrandsResponse>
	{
	}
}
