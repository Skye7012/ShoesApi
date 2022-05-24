namespace ShoesApi.Entities
{
	/// <summary>
	/// Назначение обуви
	/// </summary>
	public class Destination : EntityBase
	{
		public string Name { get; set; } = null!;


		#region navigation Properties

		public List<Shoe>? Shoes { get; set; }

		#endregion
	}
}
