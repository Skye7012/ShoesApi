namespace ShoesApi.CQRS.Queries
{
	/// <summary>
	/// Базовые фильтры для get запроса
	/// </summary>
	public class BaseGetQuery
	{
		/// <summary>
		/// Страница
		/// </summary>
		public int Page { get; set; } = 1;

		/// <summary>
		/// Лимит страницы
		/// </summary>
		public int Limit { get; set; } = 10;

		/// <summary>
		/// Поле сортировки
		/// </summary>
		public string OrderBy { get; set; } = "Id";

		/// <summary>
		/// Сортировка по возрастанию
		/// </summary>
		public bool IsAscending { get; set; } = true;
	}
}
