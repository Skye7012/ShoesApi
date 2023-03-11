namespace ShoesApi.Entities
{
	/// <summary>
	/// Сезон обуви
	/// </summary>
	public class Season : EntityBase
	{
		/// <summary>
		/// Наименование
		/// </summary>

		public string Name { get; set; } = default!;

		#region navigation Properties

		/// <summary>
		/// Кроссовки
		/// </summary>

		public List<Shoe>? Shoes { get; set; }

		#endregion
	}
}
