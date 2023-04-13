using MediatR;
using ShoesApi.Contracts.Requests.Destinations.GetDestinations;

namespace ShoesApi.Application.Destinations.Queries.GetDestinations;

/// <summary>
/// Запрос на получение коллекции Назначений обуви
/// </summary>
public class GetDestinationsQuery : IRequest<GetDestinationsResponse>
{
}
