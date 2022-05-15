using System;
using System.Collections.Generic;

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

		#endregion
	}
}
