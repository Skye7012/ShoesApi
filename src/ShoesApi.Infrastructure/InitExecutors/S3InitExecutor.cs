using HostInitActions;
using Microsoft.Extensions.Logging;
using ShoesApi.Application.Common.Interfaces;

namespace ShoesApi.Infrastructure.InitExecutors;

/// <summary>
/// Инициализатор базы данных
/// </summary>
public class S3InitExecutor : IAsyncInitActionExecutor
{
	private readonly IS3Service _s3Service;
	private readonly ILogger<S3InitExecutor> _logger;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="s3Service">S3 хранилище</param>
	/// <param name="logger">Логгер</param>
	public S3InitExecutor(
		IS3Service s3Service,
		ILogger<S3InitExecutor> logger)
	{
		_s3Service = s3Service;
		_logger = logger;
	}

	/// <summary>
	/// Провести инициализацию
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	public async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		if (await _s3Service.InitializeStorageAsync(cancellationToken))
			_logger.LogInformation("Minio bucket существует");
		else
			_logger.LogInformation("Minio bucket был создан");
	}
}
