using System;
using System.Collections.Generic;

namespace ShoesApi
{
	/// <summary>
	/// Брэнды
	/// </summary>
	public partial class Brand
	{
		public Brand()
		{
			Shoes = new HashSet<Shoe>();
		}

		public int Id { get; set; }
		public string Name { get; set; } = null!;

		public virtual ICollection<Shoe> Shoes { get; set; }
	}
}
