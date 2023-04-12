using MediatR;

namespace ShoesApi.Application.Sizes.Queries.GetSizes;

/// <summary>
/// Запрос на получение списка Размеров обуви
/// </summary>
public class GetSizesQuery : IRequest<List<int>>
{
}
