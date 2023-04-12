using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using ShoesApi.Application.Common.Extensions;
using ShoesApi.Application.Files.Commands.UploadFile;
using Xunit;

namespace ShoesApi.UnitTests.Requests.FileRequests;

/// <summary>
/// Тест для <see cref="UploadFileCommandHandler"/>
/// </summary>
public class UploadFileCommandHandlerTests : UnitTestBase
{
	/// <summary>
	/// Должен загрузить файл, если валидный запрос
	/// </summary>
	[Fact]
	public async Task UploadFileCommandHandler_ShouldUploadFile_WhenValidRequest()
	{
		var formFile = new FormFile(
			baseStream: S3ServiceTestStream,
			baseStreamOffset: 0,
			length: S3ServiceTestStream.Length,
			name: "test",
			fileName: "test.test"
		);

		using var context = CreateInMemoryContext();

		var handler = new UploadFileCommandHandler(context, S3Service);
		var createdFileId = await handler.Handle(new UploadFileCommand(formFile), default);

		var createdFile = context.Files.FirstOrDefault(x => x.Id == createdFileId);
		createdFile.Should().NotBeNull();

		createdFile!.Name.Should().Contain(formFile.FileName);

		S3ServiceTestStream.Position = 0;
		await S3Service.Received(1)
			.UploadAsync(
				Arg.Is<Stream>(x => x.StreamEquals(S3ServiceTestStream)),
				Arg.Is<string>(x => x.Contains("test.test")),
				default);
	}
}
