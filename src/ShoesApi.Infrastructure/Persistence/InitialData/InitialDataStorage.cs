using ShoesApi.Domain.Entities;
using ShoesApi.Domain.Entities.ShoeSimpleFilters;

namespace ShoesApi.Infrastructure.Persistence.InitialData;

/// <summary>
/// Хранилище изначальных данных для сидинга
/// </summary>
public static class InitialDataStorage
{
	/// <summary>
	/// Начальные Брэнды обуви
	/// </summary>
	public static readonly List<Brand> Brands = new()
	{
		new Brand { Id = 1, Name = "Nike" },
		new Brand { Id = 2, Name = "Adidas" },
		new Brand { Id = 3, Name = "Reebok" },
		new Brand { Id = 4, Name = "Asics" }
	};

	/// <summary>
	/// Начальные Назначения обуви
	/// </summary>
	public static readonly List<Destination> Destinations = new()
	{
		new Destination { Id = 1, Name = "Повседневность" },
		new Destination { Id = 2, Name = "Баскетбол" },
		new Destination { Id = 3, Name = "Волейбол" },
		new Destination { Id = 4, Name = "Бег" }
	};

	/// <summary>
	/// Начальные Сезоны обуви
	/// </summary>
	public static readonly List<Season> Seasons = new()
	{
		new Season { Id = 1, Name = "Лето" },
		new Season { Id = 2, Name = "Демисезон" },
		new Season { Id = 3, Name = "Зима" }
	};

	/// <summary>
	/// Начальные Размеры обуви
	/// </summary>
	public static readonly List<Size> Sizes = new()
	{
		new Size { Id = 1, RuSize = 38 },
		new Size { Id = 2, RuSize = 39 },
		new Size { Id = 3, RuSize = 40 },
		new Size { Id = 4, RuSize = 41 },
		new Size { Id = 5, RuSize = 42 },
	};
}
