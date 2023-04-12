using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Domain.Entities;
using ShoesApi.Infrastructure.Persistence.InitialData;

namespace ShoesApi.IntegrationTests;

/// <summary>
/// Сидер для интеграционных тестов
/// </summary>
public class IntegrationTestSeeder
{
	private readonly IApplicationDbContext _context;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="userService">Сервис пользователя</param>
	public IntegrationTestSeeder(IApplicationDbContext dbContext, IUserService userService)
	{
		_context = dbContext;

		userService.CreatePasswordHash("AdminPassword", out var passwordHash, out var passwordSalt);

		AdminUser = new User
		{
			Login = "AdminLogin",
			PasswordHash = passwordHash,
			PasswordSalt = passwordSalt,
			Name = "AdminName",
			Surname = "AdminSurname",
			Phone = "88005553535",
		};
	}

	/// <summary>
	/// Администратор
	/// </summary>
	public User AdminUser { get; }

	/// <summary>
	/// Проинициализировать БД начальными данными, которые должны быть в каждом тесте
	/// </summary>
	public async Task SeedInitialDataAsync()
	{
		await _context.Users.AddAsync(AdminUser);
		await _context.SaveChangesAsync();
	}

	/// <summary>
	/// Переинициализировать БД начальными данными, которые указаны в миграциях
	/// </summary>
	public async Task ReseedInitialDataAsync()
	{
		_context.Instance.ChangeTracker.Clear();

		await _context.Brands.AddRangeAsync(InitialDataStorage.Brands);
		await _context.Destinations.AddRangeAsync(InitialDataStorage.Destinations);
		await _context.Seasons.AddRangeAsync(InitialDataStorage.Seasons);
		await _context.Sizes.AddRangeAsync(InitialDataStorage.Sizes);

		await _context.SaveChangesAsync();
	}

	/// <summary>
	/// Добавить в БД обувь
	/// </summary>
	/// <returns>Добавленная обувь</returns>
	public async Task<List<Shoe>> SeedShoesAsync()
	{
		var imageFile = new File("test.test");
		var sizes = await _context.Sizes.ToListAsync();

		var shoes = new List<Shoe>
		{
			new Shoe
			{
				Name = "Shoe1",
				ImageFile = imageFile,
				Price = 100,
				BrandId = 1,
				DestinationId = 1,
				SeasonId = 1,
				Sizes = sizes.GetRange(0, 2),
			},
			new Shoe
			{
				Name = "Shoe2",
				ImageFile = imageFile,
				Price = 200,
				BrandId = 2,
				DestinationId = 2,
				SeasonId = 2,
				Sizes = sizes.GetRange(0, 2),
			}
		};

		await _context.Shoes.AddRangeAsync(shoes);
		await _context.SaveChangesAsync();
		return shoes;
	}
}
