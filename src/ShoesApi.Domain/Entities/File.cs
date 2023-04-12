using ShoesApi.Domain.Common;

namespace ShoesApi.Domain.Entities;

/// <summary>
/// Файл
/// </summary>
public class File : EntityBase
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="name">Наименование файла</param>
	public File(string name) => Name = name;

	/// <summary>
	/// Конструктор
	/// </summary>
	protected File() { }

	/// <summary>
	/// Наименование файла
	/// </summary>
	public string Name { get; set; } = default!;

	#region navigation Properties

	/// <summary>
	/// Кроссовки
	/// </summary>
	public List<Shoe>? Shoes { get; private set; }

	#endregion
}
