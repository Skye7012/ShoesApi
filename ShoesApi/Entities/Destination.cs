namespace ShoesApi.Entities
{
	/// <summary>
	/// Назначение обуви
	/// </summary>
	public class Destination : EntityBase
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
