using System;
using System.Collections.Generic;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Обувь из заказа
	/// </summary>
	public class OrderShoe
	{
		public int OrderId { get; set; }
		public int ShoeId { get; set; }

		public int SizeId { get; set; }

		#region navigation Properties

		public Order? Order { get; set; }

		public Shoe? Shoe { get; set; }
		public Size? Size { get; set; }

		#endregion
	}
}
