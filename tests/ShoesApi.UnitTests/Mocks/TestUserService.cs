using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Domain.Entities;

namespace ShoesApi.UnitTests.Mocks;

/// <summary>
/// Сервис пользователя для тестов
/// </summary>
public class TestUserService : IUserService
{
	/// <summary>
	/// Конструктор
	/// </summary>
	public TestUserService()
		=> AdminUser = new User
		{
			Login = "Admin",
			Name = "AdminName",
			Surname = "AdminSurname",
			PasswordHash = new byte[] { 1, 2 },
			PasswordSalt = new byte[] { 3, 4 },
			Phone = "AdminPhone",
		};

	/// <summary>
	/// Пользователь-администратор, который присутствует в тестах в БД по дефолту
	/// </summary>
	public User AdminUser { get; private set; }

	/// <summary>
	/// Получить тестовый пароль
	/// </summary>
	/// <returns>Тестовый пароль</returns>
	public string AdminUserPassword => "AdminPassword";

	/// <inheritdoc/>
	public string GetLogin()
		=> AdminUser.Login;

	/// <inheritdoc/>
	public string CreateToken(User user) => "token";

	/// <inheritdoc/>
	public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
	{
		passwordHash = new byte[] { 1, 2 };
		passwordSalt = new byte[] { 3, 4 };
	}

	/// <inheritdoc/>
	public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		=> password == AdminUserPassword;
}
