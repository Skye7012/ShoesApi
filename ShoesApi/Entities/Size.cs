namespace ShoesApi.Entities
{
	/// <summary>
	/// Размер обувь
	/// </summary>
	public class Size : EntityBase
	{
		/// <summary>
		/// RuSize
		/// </summary>
		public int RuSize { get; set; }


		#region navigation Properties

		/// <summary>
		/// Shoes
		/// </summary>
		public List<Shoe>? Shoes { get; set; }

		/// <summary>
		/// OrderItems
		/// </summary>
		public List<OrderItem>? OrderItems { get; set; }

		#endregion
	}
}
