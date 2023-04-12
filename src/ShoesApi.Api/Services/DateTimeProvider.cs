using ShoesApi.Application.Common.Interfaces;

namespace ShoesApi.Api.Services;

/// <inheritdoc/>
public class DateTimeProvider : IDateTimeProvider
{
	/// <inheritdoc/>
	public DateTime UtcNow => DateTime.UtcNow;
}
