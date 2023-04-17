using MediatR;

namespace ShoesApi.Application.Shoes.Commands.DeleteShoe;

/// <summary>
/// Команда для удаление обуви
/// </summary>
public class DeleteShoeCommand : IRequest
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="id">Идентификатор</param>
	public DeleteShoeCommand(int id)
		=> Id = id;

	/// <summary>
	/// Идентификатор
	/// </summary>
	public int Id { get; set; }
}
