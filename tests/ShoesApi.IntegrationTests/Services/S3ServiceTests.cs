using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using ShoesApi.Infrastructure.Persistence;
using Xunit;

namespace ShoesApi.IntegrationTests.Services;

/// <summary>
/// Тесты для <see cref="S3Service"/>
/// </summary>
public class S3ServiceTests : IntegrationTestsBase
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="factory">Фабрика приложения</param>
	public S3ServiceTests(IntegrationTestFactory<Program, ApplicationDbContext> factory) : base(factory)
	{
	}

	/// <summary>
	/// Должен создавать бакет files при запуске приложения
	/// </summary>
	[Fact]
	public async Task InitializeStorageAsync_ShouldCreateFilesBucket_WhenApplicationStarts()
	{
		var minioClient = _factory.Services.GetRequiredService<MinioClient>();
		var isBucketCreated = await minioClient.BucketExistsAsync(
			new BucketExistsArgs()
				.WithBucket(Infrastructure.Services.S3Service.BucketName));

		isBucketCreated.Should().BeTrue();
	}
}
