namespace ShoesApi.Entities
{
	/// <summary>
	/// Заказ
	/// </summary>
	public class Order : EntityBase
	{
		public DateTime OrderDate { get; set; }

		public string Addres { get; set; } = default!;

		public int Sum { get; set; }

		public int Count { get; set; }

		public int UserId { get; set; }


		#region navigation Properties

		public User? User { get; set; }

		public List<Shoe>? Shoes { get; set; }
		public List<OrderItem>? OrderItems { get; set; }

		#endregion
	}
}
