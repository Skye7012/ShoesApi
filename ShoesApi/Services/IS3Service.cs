namespace ShoesApi.Services
{
	/// <inheritdoc/>
	public interface IS3Service
	{
		/// <summary>
		/// Загрузить файл
		/// </summary>
		/// <param name="stream">Файл</param>
		/// <param name="fileName">Наименование файла</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns></returns>
		public Task UploadAsync(
			Stream stream,
			string fileName,
			CancellationToken cancellationToken);

		/// <summary>
		/// Скачать файл
		/// </summary>
		/// <param name="fileName">Наименование файла</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns></returns>
		public Task<Stream> DownloadAsync(
			string fileName,
			CancellationToken cancellationToken);

		/// <summary>
		/// Проверить существование бакета, и при отсутствии оного создать<br/>
		/// Вызывается один раз при запуске веб приложения
		/// </summary>
		public Task InitializeStorageAsync();
	}
}
