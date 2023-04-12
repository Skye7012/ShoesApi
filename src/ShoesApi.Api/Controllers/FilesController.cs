using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.Application.Files.Commands.UploadFile;
using ShoesApi.Application.Files.Queries.DownloadFile;

namespace ShoesApi.Api.Controllers;

/// <summary>
/// Контроллер Файлов
/// </summary>
[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
	private readonly IMediator _mediator;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="mediator">Медиатор</param>
	public FilesController(IMediator mediator)
		=> _mediator = mediator;

	/// <summary>
	/// Скачать файл
	/// </summary>
	/// <param name="id">Идентификатор файла</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Файл</returns>
	[HttpGet("Download/{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<FileStreamResult> DownloadAsync(
		int id,
		CancellationToken cancellationToken)
			=> await _mediator.Send(new DownloadFileQuery(id), cancellationToken);

	/// <summary>
	/// Загрузить файл
	/// </summary>
	/// <param name="file">Файл</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Идентификатор файла</returns>
	[HttpPost("Upload")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	public async Task<int> UploadAsync(
		IFormFile file,
		CancellationToken cancellationToken)
			=> await _mediator.Send(new UploadFileCommand(file), cancellationToken);
}
