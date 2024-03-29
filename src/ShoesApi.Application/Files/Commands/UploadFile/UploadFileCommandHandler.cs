using MediatR;
using ShoesApi.Application.Common.Extensions;
using ShoesApi.Application.Common.Interfaces;
using File = ShoesApi.Domain.Entities.File;

namespace ShoesApi.Application.Files.Commands.UploadFile;

/// <summary>
/// Обработчик для <see cref="UploadFileCommand"/>
/// </summary>
public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, int>
{
	private readonly IApplicationDbContext _context;
	private readonly IS3Service _s3Service;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	/// <param name="s3Service">S3 хранилище</param>
	public UploadFileCommandHandler(
		IApplicationDbContext context,
		IS3Service s3Service)
	{
		_context = context;
		_s3Service = s3Service;
	}

	/// <inheritdoc/>
	public async Task<int> Handle(UploadFileCommand request, CancellationToken cancellationToken)
	{
		var fileName = $"{Guid.NewGuid()}_{request.File.FileName}";

		await _s3Service.UploadAsync(
			request.File.GetStream(),
			fileName,
			cancellationToken);

		var file = new File(fileName);
		await _context.Files.AddAsync(file, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return file.Id;
	}
}
