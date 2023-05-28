namespace ShoesApi.Application.Common.Interfaces;

/// <summary>
/// Сервис для работы с S3 хранилищем
/// </summary>
public interface IS3Service
{
	/// <summary>
	/// Загрузить файл
	/// </summary>
	/// <param name="stream">Файл</param>
	/// <param name="fileName">Наименование файла</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns></returns>
	Task UploadAsync(
		Stream stream,
		string fileName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Скачать файл
	/// </summary>
	/// <param name="fileName">Наименование файла</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns></returns>
	Task<Stream> DownloadAsync(
		string fileName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Проверить существование бакета, и при отсутствии оного создать<br/>
	/// Вызывается один раз при запуске веб приложения
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Существовал ли бакет</returns>
	Task<bool> InitializeStorageAsync(CancellationToken cancellationToken);
}
