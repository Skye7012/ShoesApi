using System.Net;

namespace ShoesApi.Exceptions
{
	/// <summary>
	/// Ошибка валидации
	/// </summary>
	public class ValidationException : ApplicationExceptionBase
	{

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="message">Сообщение</param>
		public ValidationException(string message) : base(message)
		{ }

		/// <summary>
		/// Код состояния
		/// </summary>
		public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
	}
}
