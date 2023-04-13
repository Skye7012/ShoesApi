using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Domain.Entities;

namespace ShoesApi.Application.Shoes.Commands.DeleteShoe;

/// <summary>
/// Обработчик для <see cref="DeleteShoeCommand"/>
/// </summary>
public class DeleteShoeCommandHandler : IRequestHandler<DeleteShoeCommand>
{
	private readonly IApplicationDbContext _context;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	public DeleteShoeCommandHandler(IApplicationDbContext context)
		=> _context = context;

	/// <inheritdoc/>
	public async Task Handle(DeleteShoeCommand request, CancellationToken cancellationToken)
	{
		var shoe = await _context.Shoes
			.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
			?? throw new EntityNotFoundException<Shoe>(request.Id);

		_context.Shoes.Remove(shoe);
		await _context.SaveChangesAsync(cancellationToken);
	}
}
