﻿namespace ShoesApi.CQRS.Queries.Shoes.GetShoes
{
	/// <summary>
	/// ДТО Брэнда обуви из <see cref="GetShoesResponseItem"/>
	/// </summary>
	public class GetShoesResponseItemBrand
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Наименование
		/// </summary>
		public string Name { get; set; } = default!;
	}
}
