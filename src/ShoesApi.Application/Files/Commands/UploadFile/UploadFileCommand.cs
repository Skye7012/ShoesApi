using MediatR;
using Microsoft.AspNetCore.Http;

namespace ShoesApi.Application.Files.Commands.UploadFile;

/// <summary>
/// Команда для загрузки файла
/// </summary>
public class UploadFileCommand : IRequest<int>
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="file">Файл</param>
	public UploadFileCommand(IFormFile file)
		=> File = file;

	/// <summary>
	/// Файл
	/// </summary>
	public IFormFile File { get; set; }
}
