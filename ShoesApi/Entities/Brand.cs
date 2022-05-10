using System;
using System.Collections.Generic;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Брэнды
	/// </summary>
	public class Brand : EntityBase
	{
		public Brand()
		{
			Shoes = new HashSet<Shoe>();
		}

		public string Name { get; set; } = null!;

		public virtual ICollection<Shoe> Shoes { get; set; }
	}
}
