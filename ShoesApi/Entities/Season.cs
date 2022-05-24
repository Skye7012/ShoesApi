namespace ShoesApi.Entities
{
	/// <summary>
	/// Сезон
	/// </summary>
	public class Season : EntityBase
	{
		public string Name { get; set; } = null!;


		#region navigation Properties

		public List<Shoe>? Shoes { get; set; }

		#endregion
	}
}
