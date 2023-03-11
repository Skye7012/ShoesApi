namespace ShoesApi.Entities
{
	/// <summary>
	/// Назначение обуви
	/// </summary>
	public class Destination : EntityBase
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
