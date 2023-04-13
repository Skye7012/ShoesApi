using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;
using File = ShoesApi.Domain.Entities.File;

namespace ShoesApi.Application.Files.Queries.DownloadFile;

/// <summary>
/// Обработчик для of <see cref="DownloadFileQuery"/>
/// </summary>
public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, FileStreamResult>
{
	private readonly IApplicationDbContext _context;
	private readonly IS3Service _s3Service;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	/// <param name="s3Service">S3 хранилище</param>
	public DownloadFileQueryHandler(IApplicationDbContext context, IS3Service s3Service)
	{
		_context = context;
		_s3Service = s3Service;
	}

	/// <inheritdoc/>
	public async Task<FileStreamResult> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
	{
		var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
			?? throw new EntityNotFoundException<File>(request.Id);

		var stream = await _s3Service.DownloadAsync(file.Name, cancellationToken);

		return new FileStreamResult(stream, MediaTypeNames.Application.Octet)
		{
			FileDownloadName = file.Name,
		};
	}
}
