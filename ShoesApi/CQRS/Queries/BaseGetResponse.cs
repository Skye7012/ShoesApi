namespace ShoesApi.CQRS.Queries
{
	/// <summary>
	/// Базовый класс ответа на гет запрос
	/// </summary>
	public class BaseGetResponse<T>
		where T : class
	{
		/// <summary>
		/// Количество объектов
		/// </summary>
		public int TotalCount { get; set; }

		/// <summary>
		/// Объекты
		/// </summary>
		public List<T>? Items { get; set; }
	}
}
