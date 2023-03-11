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
		/// Путь для изображения // TODO: change
		/// </summary>
		public string Image { get; set; } = default!;

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
		/// Брэнд обуви
		/// </summary>
		public virtual Brand? Brand { get; set; }

		/// <summary>
		/// Назначение обуви
		/// </summary>
		public virtual Destination? Destination { get; set; }

		/// <summary>
		/// Сезон обуви
		/// </summary>
		public virtual Season? Season { get; set; }

		/// <summary>
		/// Размеры обуви
		/// </summary>
		public virtual List<Size>? Sizes { get; set; }

		/// <summary>
		/// Части заказа
		/// </summary>
		public List<OrderItem>? OrderItems { get; set; }

		#endregion
	}
}
