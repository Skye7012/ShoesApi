namespace ShoesApi.Entities
{
	/// <summary>
	/// Заказ
	/// </summary>
	public class Order : EntityBase
	{
		/// <summary>
		/// OrderDate
		/// </summary>
		
		public DateTime OrderDate { get; set; }

		/// <summary>
		/// Address
		/// </summary>
		
		public string Address { get; set; } = default!;

		/// <summary>
		/// Sum
		/// </summary>
		
		public int Sum { get; set; }

		/// <summary>
		/// Count
		/// </summary>
		
		public int Count { get; set; }

		/// <summary>
		/// UserId
		/// </summary>
		
		public int UserId { get; set; }


		#region navigation Properties

		/// <summary>
		/// User
		/// </summary>
		
		public User? User { get; set; }

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
