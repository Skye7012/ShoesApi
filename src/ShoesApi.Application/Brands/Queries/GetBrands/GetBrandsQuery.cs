using MediatR;
using ShoesApi.Contracts.Requests.Brands.GetBrands;

namespace ShoesApi.Application.Brands.Queries.GetBrands;

/// <summary>
/// Запрос на получение коллекции Брэндов обуви
/// </summary>
public class GetBrandsQuery : IRequest<GetBrandsResponse>
{
}
