namespace ShoesApi.Entities
{
	/// <summary>
	/// Брэнды обуви
	/// </summary>
	public class Brand : EntityBase
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
