using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShoesApi.Application.Files.Queries.DownloadFile;

/// <summary>
/// Запрос на скачивание файла 
/// </summary>
public class DownloadFileQuery : IRequest<FileStreamResult>
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="id">Идентификатор</param>
	public DownloadFileQuery(int id)
		=> Id = id;

	/// <summary>
	/// Идентификатор файла
	/// </summary>
	public int Id { get; set; }
}
