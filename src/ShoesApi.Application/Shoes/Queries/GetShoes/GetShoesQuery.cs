using MediatR;
using ShoesApi.Contracts.Requests.Shoes.Common;
using ShoesApi.Contracts.Requests.Shoes.GetShoes;

namespace ShoesApi.Application.Shoes.Queries.GetShoes;

/// <summary>
/// Запрос на получение коллекции обуви 
/// </summary>
public class GetShoesQuery : GetShoesRequest, IRequest<GetShoesResponse>
{
}
