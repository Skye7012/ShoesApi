using System;
using System.Collections.Generic;

namespace ShoesApi
{
	/// <summary>
	/// Сезон
	/// </summary>
	public partial class Season
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
