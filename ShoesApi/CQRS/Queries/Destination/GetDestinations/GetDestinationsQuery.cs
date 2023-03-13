using MediatR;

namespace ShoesApi.CQRS.Queries.Destination.GetDestinations
{
	/// <summary>
	/// Запрос на получение коллекции Назначений обуви
	/// </summary>
	public class GetDestinationsQuery : IRequest<GetDestinationsResponse>
	{
	}
}
