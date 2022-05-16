using System;
using System.Collections.Generic;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Обувь
	/// </summary>
	public class Shoe : EntityBase
	{
		public string Name { get; set; } = null!;

		/// <summary>
		/// Название изображения (для пути)
		/// </summary>
		public string Image { get; set; } = null!;

		/// <summary>
		/// Цена
		/// </summary>
		public int Price { get; set; }

		public int? BrandId { get; set; }
		public int? DestinationId { get; set; }
		public int? SeasonId { get; set; }

		#region navigation Properties

		public virtual Brand? Brand { get; set; }
		public virtual Destination? Destination { get; set; }
		public virtual Season? Season { get; set; }
		public virtual List<Size>? Sizes { get; set; }
		public virtual List<Order>? Orders { get; set; }

		public List<OrderShoe>? OrderShoes { get; set; }

		#endregion
	}
}
