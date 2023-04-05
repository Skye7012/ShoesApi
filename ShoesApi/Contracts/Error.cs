namespace ShoesApi.Exceptions
{
	/// <summary>
	/// Ошибка
	/// </summary>
	public class Error
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="message">Сообщение</param>
		public Error(string message) => Message = message;

		/// <summary>
		/// Сообщение
		/// </summary>
		public string Message { get; }
	}
}
