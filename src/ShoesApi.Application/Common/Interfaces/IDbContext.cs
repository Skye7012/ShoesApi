using Microsoft.EntityFrameworkCore;

namespace ShoesApi.Application.Common.Interfaces;

/// <summary>
/// Интерфейс контекста БД
/// </summary>
public interface IDbContext : IDisposable
{
	/// <summary>
	/// Экземпляр текущего контекста БД
	/// </summary>
	DbContext Instance { get; }
}
