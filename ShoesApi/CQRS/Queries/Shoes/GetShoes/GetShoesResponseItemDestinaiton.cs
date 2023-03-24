﻿namespace ShoesApi.CQRS.Queries.Shoes.GetShoes
{
	/// <summary>
	/// ДТО Назначения обуви из <see cref="GetShoesResponseItem"/>
	/// </summary>
	public class GetShoesResponseItemDestination
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
