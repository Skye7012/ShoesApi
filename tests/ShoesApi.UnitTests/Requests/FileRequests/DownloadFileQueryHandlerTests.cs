using System.Net.Mime;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Files.Queries.DownloadFile;
using ShoesApi.Domain.Entities;
using Xunit;

namespace ShoesApi.UnitTests.Requests.FileRequests;

/// <summary>
/// Тест для <see cref="DownloadFileQueryHandler"/>
/// </summary>
public class DownloadFileQueryHandlerTests : UnitTestBase
{
	/// <summary>
	/// Должен скачать файл, если он существует в БД
	/// </summary>
	[Fact]
	public async Task DownloadFileQueryHandler_ShouldDownloadFile_WhenFileExists()
	{
		var file = new File("test");

		using var context = CreateInMemoryContext(c =>
		{
			c.Files.Add(file);
			c.SaveChanges();
		});

		var handler = new DownloadFileQueryHandler(context, S3Service);
		var result = await handler.Handle(new DownloadFileQuery(file.Id), default);

		result.Should().NotBeNull();
		result.FileDownloadName.Should().Be("test");
		result.ContentType.Should().Be(MediaTypeNames.Application.Octet);

		result.FileStream.Should().BeSameAs(S3ServiceTestStream);

		await S3Service.Received(1)
			.DownloadAsync(file.Name, default);
	}

	/// <summary>
	/// Должен выкинуть ошибку, если они файл не существует в БД
	/// </summary>
	[Fact]
	public async Task DownloadFileQueryHandler_ShouldThrow_WhenFileDoesntExist()
	{
		using var context = CreateInMemoryContext();

		var handler = new DownloadFileQueryHandler(context, S3Service);
		var handle = async () => await handler.Handle(new DownloadFileQuery(404), default);

		await handle.Should()
			.ThrowAsync<EntityNotFoundException<File>>();
	}
}
