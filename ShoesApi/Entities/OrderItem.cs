namespace ShoesApi.Entities
{
	/// <summary>
	/// Часть заказа
	/// </summary>
	public class OrderItem : EntityBase
	{
		/// <summary>
		/// OrderId
		/// </summary>
		
		public int OrderId { get; set; }
		
		/// <summary>
		/// ShoeId
		/// </summary>
		
		public int ShoeId { get; set; }

		/// <summary>
		/// SizeId
		/// </summary>
		
		public int SizeId { get; set; }

		#region navigation Properties

		/// <summary>
		/// Order
		/// </summary>
		
		public Order? Order { get; set; }

		/// <summary>
		/// Shoe
		/// </summary>
		
		public Shoe? Shoe { get; set; }

		/// <summary>
		/// Size
		/// </summary>
		
		public Size? Size { get; set; }

		#endregion
	}
}
