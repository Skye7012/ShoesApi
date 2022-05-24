namespace ShoesApi.Entities
{
	/// <summary>
	/// Размер обувь
	/// </summary>
	public class Size : EntityBase
	{
		public int RuSize { get; set; }


		#region navigation Properties

		public List<Shoe>? Shoes { get; set; }

		public List<OrderItem>? OrderItems { get; set; }

		#endregion
	}
}
