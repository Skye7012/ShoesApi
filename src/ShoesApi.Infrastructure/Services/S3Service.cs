using Minio;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;

namespace ShoesApi.Infrastructure.Services;

/// <inheritdoc/>
public class S3Service : IS3Service
{
	/// <summary>
	/// Наименование бакета
	/// </summary>
	public const string BucketName = "files";

	private readonly MinioClient _minioClient;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="minioClient">Клиент minio</param>
	public S3Service(MinioClient minioClient)
		=> _minioClient = minioClient;

	/// <inheritdoc/>
	public async Task UploadAsync(
		Stream data,
		string fileName,
		CancellationToken cancellationToken)
	{
		await _minioClient.PutObjectAsync(
			new PutObjectArgs()
				.WithBucket(BucketName)
				.WithObject(fileName)
				.WithObjectSize(data.Length)
				.WithStreamData(data),
			cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<Stream> DownloadAsync(
		string fileName,
		CancellationToken cancellationToken)
	{
		Stream data = new MemoryStream();

		await _minioClient.StatObjectAsync(
			new StatObjectArgs()
				.WithBucket(BucketName)
				.WithObject(fileName),
			cancellationToken);

		await _minioClient.GetObjectAsync(
			new GetObjectArgs()
				.WithBucket(BucketName)
				.WithObject(fileName)
				.WithCallbackStream(res => res.CopyTo(data)),
			cancellationToken);

		if (data.Length == 0)
			throw new ApplicationExceptionBase("Не удалось скачать файл");

		data.Position = 0;
		return data;
	}

	/// <inheritdoc/>
	public async Task<bool> InitializeStorageAsync(CancellationToken cancellationToken)
	{
		var isBucketCreated = await _minioClient.BucketExistsAsync(
			new BucketExistsArgs()
				.WithBucket(BucketName),
			cancellationToken);

		if (!isBucketCreated)
			await _minioClient.MakeBucketAsync(
				new MakeBucketArgs()
					.WithBucket(BucketName),
				cancellationToken);

		return isBucketCreated;
	}
}
