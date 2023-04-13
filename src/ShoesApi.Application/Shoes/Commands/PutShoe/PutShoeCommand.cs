using MediatR;
using ShoesApi.Contracts.Requests.Shoes.PutShoe;

namespace ShoesApi.Application.Shoes.Commands.PutShoe;

/// <summary>
/// Команда для обновления обуви
/// </summary>
public class PutShoeCommand : PutShoeRequest, IRequest
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="id">Идентификатор</param>
	public PutShoeCommand(int id)
		=> Id = id;

	/// <summary>
	/// Идентификатор
	/// </summary>
	public int Id { get; set; }
}
