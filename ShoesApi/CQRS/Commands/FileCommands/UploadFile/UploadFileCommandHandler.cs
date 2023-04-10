using MediatR;
using ShoesApi.Extensions;
using ShoesApi.Services;
using File = ShoesApi.Entities.File;

namespace ShoesApi.CQRS.Commands.FileCommands.UploadFile
{
	/// <summary>
	/// Обработчик для <see cref="UploadFileCommand"/>
	/// </summary>
	public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, int>
	{
		private readonly ShoesDbContext _context;
		private readonly IS3Service _s3Service;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		/// <param name="s3Service">S3 хранилище</param>
		public UploadFileCommandHandler(
			ShoesDbContext context,
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
}
