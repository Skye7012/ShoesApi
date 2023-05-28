using System.Net;

namespace ShoesApi.Application.Common.Exceptions;

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

	/// <inheritdoc/>
	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
