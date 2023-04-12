using MediatR;

namespace ShoesApi.Application.Seasons.Queries.GetSeasons;

/// <summary>
/// Запрос на получение коллекции Сезонов обуви
/// </summary>
public class GetSeasonsQuery : IRequest<GetSeasonsResponse>
{
}
