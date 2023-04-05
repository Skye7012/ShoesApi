using ShoesApi.Entities;

namespace ShoesApi.Services
{
	/// <summary>
	/// Сервис пользователя
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// Получить логин по клейму в токене
		/// </summary>
		/// <returns>Логин</returns>
		public string GetLogin();

		/// <summary>
		/// Создать токен авторизации
		/// </summary>
		/// <param name="user">Пользователь, для которого создается токен</param>
		/// <returns>Токен авторизации</returns>
		public string CreateToken(User user);

		/// <summary>
		/// Создать хэш пароля
		/// </summary>
		/// <param name="password">Пароль</param>
		/// <param name="passwordHash">Итоговый захэшированный пароль</param>
		/// <param name="passwordSalt">Соль захэшированного пароля</param>
		public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

		/// <summary>
		/// Проверить валидность пароля
		/// </summary>
		/// <param name="password">Пароль</param>
		/// <param name="passwordHash">Хэш пароля</param>
		/// <param name="passwordSalt">Соль пароля</param>
		/// <returns></returns>
		public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
	}
}
