namespace ShoesApi.Services
{
	/// <summary>
	/// Мокируемый провайдер для <see cref="DateTime"/>
	/// </summary>
	public interface IDateTimeProvider
	{
		/// <summary>
		/// Мокируемый <see cref="DateTime.UtcNow"/>
		/// </summary>
		public DateTime UtcNow { get; }
	}
}
