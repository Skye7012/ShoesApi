using System.Net;
using ShoesApi.Domain.Common;

namespace ShoesApi.Application.Common.Exceptions;

/// <summary>
/// Сущность не найдена
/// </summary>
public class EntityNotFoundException<TEntity> : ApplicationExceptionBase
	where TEntity : EntityBase
{

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="message">Сообщение</param>
	public EntityNotFoundException(string message) : base(message)
	{ }

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="id">Идентификатор</param>
	public EntityNotFoundException(int id)
		: base($"Не удалось найти сущность '{typeof(TEntity).Name}' по id = '{id}'")
	{ }

	/// <inheritdoc/>
	public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}
