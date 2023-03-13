using ShoesApi.CQRS.Queries;

namespace ShoesApi.CQRS.Queries.Season.GetSeasons
{
	/// <summary>
	/// Ответ на <see cref="GetSeasonsQuery"/>
	/// </summary>
	public class GetSeasonsResponse : BaseGetResponse<GetSeasonsResponseItem>
	{
	}
}
