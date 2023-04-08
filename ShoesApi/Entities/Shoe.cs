using ShoesApi.Entities.ShoeSimpleFilters;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Обувь
	/// </summary>
	public class Shoe : EntityBase
	{
		/// <summary>
		/// Наименование
		/// </summary>
		public string Name { get; set; } = default!;

		/// <summary>
		/// Идентификатор файла изображения
		/// </summary>
		public int ImageFileId { get; set; }

		/// <summary>
		/// Цена
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// Идентификатор Брэнда обуви
		/// </summary>
		public int? BrandId { get; set; }

		/// <summary>
		/// Идентификатор Назначения обуви
		/// </summary>
		public int? DestinationId { get; set; }

		/// <summary>
		/// Идентификатор Сезона обуви
		/// </summary>
		public int? SeasonId { get; set; }

		#region navigation Properties

		/// <summary>
		/// Файл изображения
		/// </summary>
		public File? ImageFile { get; set; }

		/// <summary>
		/// Брэнд обуви
		/// </summary>
		public Brand? Brand { get; set; }

		/// <summary>
		/// Назначение обуви
		/// </summary>
		public Destination? Destination { get; set; }

		/// <summary>
		/// Сезон обуви
		/// </summary>
		public Season? Season { get; set; }

		/// <summary>
		/// Размеры обуви
		/// </summary>
		public List<Size>? Sizes { get; set; }

		/// <summary>
		/// Части заказа
		/// </summary>
		public List<OrderItem>? OrderItems { get; private set; }

		#endregion
	}
}
