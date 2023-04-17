using MediatR;
using ShoesApi.Contracts.Requests.Seasons.GetSeasons;

namespace ShoesApi.Application.Seasons.Queries.GetSeasons;

/// <summary>
/// Запрос на получение коллекции Сезонов обуви
/// </summary>
public class GetSeasonsQuery : IRequest<GetSeasonsResponse>
{
}
