namespace ShoesApi.Entities
{
	/// <summary>
	/// Обувь
	/// </summary>
	public class Shoe : EntityBase
	{
		/// <summary>
		/// Name
		/// </summary>
		/// <value></value>
		public string Name { get; set; } = null!;

		/// <summary>
		/// Image name (for path)
		/// </summary>
		public string Image { get; set; } = null!;

		/// <summary>
		/// Pice
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// BrandId
		/// </summary>
		public int? BrandId { get; set; }

		/// <summary>
		/// DestinationId
		/// </summary>
		public int? DestinationId { get; set; }

		/// <summary>
		/// SeasonId
		/// </summary>
		public int? SeasonId { get; set; }

		#region navigation Properties

		/// <summary>
		/// Brand
		/// </summary>
		public virtual Brand? Brand { get; set; }

		/// <summary>
		/// Destination
		/// </summary>
		public virtual Destination? Destination { get; set; }

		/// <summary>
		/// Season
		/// </summary>
		public virtual Season? Season { get; set; }

		/// <summary>
		/// Sizes
		/// </summary>
		public virtual List<Size>? Sizes { get; set; }

		/// <summary>
		/// Orders
		/// </summary>
		public virtual List<Order>? Orders { get; set; }

		/// <summary>
		/// OrderItems
		/// </summary>
		public List<OrderItem>? OrderItems { get; set; }

		#endregion
	}
}
