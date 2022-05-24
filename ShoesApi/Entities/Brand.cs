namespace ShoesApi.Entities
{
	/// <summary>
	/// Брэнды
	/// </summary>
	public class Brand : EntityBase
	{
		public string Name { get; set; } = null!;


		#region navigation Properties

		public List<Shoe>? Shoes { get; set; }

		#endregion
	}
}
