using System.Net;

namespace ShoesApi.Application.Common.Exceptions;

/// <summary>
/// Базовая ошибка приложения
/// </summary>
public class ApplicationExceptionBase : Exception
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="message">Сообщение</param>
	public ApplicationExceptionBase(string message) : base(message)
	{ }

	/// <summary>
	/// Код состояния
	/// </summary>
	public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
