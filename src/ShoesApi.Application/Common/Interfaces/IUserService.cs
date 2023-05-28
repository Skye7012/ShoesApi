using ShoesApi.Domain.Entities;

namespace ShoesApi.Application.Common.Interfaces;

/// <summary>
/// Сервис пользователя
/// </summary>
public interface IUserService
{
	/// <summary>
	/// Получить логин по клейму в токене
	/// </summary>
	/// <returns>Логин</returns>
	string GetLogin();

	/// <summary>
	/// Создать токен авторизации
	/// </summary>
	/// <param name="user">Пользователь, для которого создается токен</param>
	/// <returns>Токен авторизации</returns>
	string CreateToken(User user);

	/// <summary>
	/// Создать хэш пароля
	/// </summary>
	/// <param name="password">Пароль</param>
	/// <param name="passwordHash">Итоговый захэшированный пароль</param>
	/// <param name="passwordSalt">Соль захэшированного пароля</param>
	void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

	/// <summary>
	/// Проверить валидность пароля
	/// </summary>
	/// <param name="password">Пароль</param>
	/// <param name="passwordHash">Хэш пароля</param>
	/// <param name="passwordSalt">Соль пароля</param>
	/// <returns>Валиден ли пароль</returns>
	bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
}
