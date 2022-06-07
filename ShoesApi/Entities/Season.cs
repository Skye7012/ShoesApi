namespace ShoesApi.Entities
{
	/// <summary>
	/// Сезон
	/// </summary>
	public class Season : EntityBase
	{
		/// <summary>
		/// Name
		/// </summary>
		
		public string Name { get; set; } = null!;


		#region navigation Properties

		/// <summary>
		/// Shoes
		/// </summary>
		
		public List<Shoe>? Shoes { get; set; }

		#endregion
	}
}
