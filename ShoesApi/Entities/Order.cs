using System;
using System.Collections.Generic;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Заказ
	/// </summary>
	public class Order : EntityBase
	{
		public DateTime OrderDate { get; set; }

		public int Sum { get; set; }

		public int Count { get; set; }


		#region navigation Properties

		public List<Shoe>? Shoes { get; set; }
		public List<OrderShoe>? OrderShoes { get; set; }

		#endregion
	}
}
