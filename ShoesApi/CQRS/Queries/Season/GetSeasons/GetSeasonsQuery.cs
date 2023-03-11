using MediatR;

namespace ShoesApi.CQRS.Queries.Season.GetSeasons
{
	/// <summary>
	/// Запрос на получение коллекции Сезонов обуви
	/// </summary>
	public class GetSeasonsQuery : IRequest<GetSeasonsResponse>
	{
	}
}
