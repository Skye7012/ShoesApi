using MediatR;

namespace ShoesApi.CQRS.Commands.FileCommands.UploadFile
{
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
}
