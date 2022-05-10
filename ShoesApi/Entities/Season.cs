using System;
using System.Collections.Generic;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Сезон
	/// </summary>
	public class Season
	{
		public Season()
		{
			Shoes = new HashSet<Shoe>();
		}

		public int Id { get; set; }
		public string Name { get; set; } = null!;

		public virtual ICollection<Shoe> Shoes { get; set; }
	}
}
