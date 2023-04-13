using MediatR;

namespace ShoesApi.Application.Brands.Queries.GetBrands;

/// <summary>
/// Запрос на получение коллекции Брэндов обуви
/// </summary>
public class GetBrandsQuery : IRequest<GetBrandsResponse>
{
}
