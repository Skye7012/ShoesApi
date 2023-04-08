using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using ShoesApi.Controllers;
using Xunit;
using File = ShoesApi.Entities.File;

namespace ShoesApi.IntegrationTests.Controllers
{
	/// <summary>
	/// Тесты для <see cref="FilesController"/>
	/// </summary>
	public class FilesControllerTests : IntegrationTestsBase
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="factory">Фабрика приложения</param>
		public FilesControllerTests(IntegrationTestFactory<Program, ShoesDbContext> factory) : base(factory)
		{
		}

		/// <summary>
		/// Загружает файл, когда запрос валиден
		/// </summary>
		[Fact]
		public async Task UploadAsync_UploadFile_WhenRequestValid()
		{
			var file = new FormFile(
				baseStream: S3ServiceTestStream,
				baseStreamOffset: 0,
				length: S3ServiceTestStream.Length,
				name: "test",
				fileName: "test.test"
			);

			var streamContent = new StreamContent(file.OpenReadStream());

			var formData = new MultipartFormDataContent();
			formData.Add(streamContent, "file", file.FileName);

			var createdFileId = await Client.PostAsync("/Files/Upload", formData)
				.GetResponseAsyncAs<int>();

			var createdFile = DbContext.Files.FirstOrDefault(x => x.Id == createdFileId);

			createdFile.Should().NotBeNull();
			createdFile!.Name.Should().Be(file.FileName);
		}

		/// <summary>
		/// Скачивает файл, когда он существует в БД и в хранилище
		/// </summary>
		[Fact]
		public async Task DownloadAsync_DownloadFile_WhenFileExists()
		{
			var file = new File("test.test");
			await DbContext.AddAsync(file);
			await DbContext.SaveChangesAsync();

			await S3Service.UploadAsync(S3ServiceTestStream, "test.test", default);

			var response = await Client.GetAsync($"/Files/Download/{file.Id}");

			response.Content.Headers.ContentDisposition!.DispositionType.Should().Be("attachment");
			response.Content.Headers.ContentDisposition!.FileName.Should().Be("test.test");

			MemoryStream stream = (MemoryStream)response.Content.ReadAsStream();
			stream.ToArray().Should().Equal(S3ServiceTestStream.ToArray());
		}
	}
}
