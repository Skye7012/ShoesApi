namespace ShoesApi.Entities.ShoeSimpleFilters
{
	/// <summary>
	/// Простой фильтр для Обуви только с наименованием фильтра
	/// </summary>
	public class ShoeSimpleFilter : EntityBase
	{
		/// <summary>
		/// Наименование
		/// </summary>
		public string Name { get; set; } = default!;

		#region navigation Properties

		/// <summary>
		/// Кроссовки
		/// </summary>
		public List<Shoe>? Shoes { get; private set; }

		#endregion
	}
}
