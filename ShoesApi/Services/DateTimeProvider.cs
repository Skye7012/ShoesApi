namespace ShoesApi.Services
{
	/// <inheritdoc/>
	public class DateTimeProvider : IDateTimeProvider
	{
		/// <inheritdoc/>
		public DateTime UtcNow => DateTime.UtcNow;
	}
}
