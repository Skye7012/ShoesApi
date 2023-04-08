using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Exceptions;
using ShoesApi.Services;
using File = ShoesApi.Entities.File;

namespace ShoesApi.CQRS.Queries.Files.DownloadFile
{
	/// <summary>
	/// Обработчик для of <see cref="DownloadFileQuery"/>
	/// </summary>
	public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, FileStreamResult>
	{
		private readonly ShoesDbContext _context;
		private readonly IS3Service _s3Service;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		/// <param name="s3Service">S3 хранилище</param>
		public DownloadFileQueryHandler(ShoesDbContext context, IS3Service s3Service)
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
}
