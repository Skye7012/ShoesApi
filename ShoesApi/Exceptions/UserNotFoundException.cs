﻿using ShoesApi.Entities;

namespace ShoesApi.Exceptions
{
	/// <summary>
	/// Не найден пользователь
	/// </summary>
	public class UserNotFoundException : EntityNotFoundException<User>
	{

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="login">Логин</param>
		public UserNotFoundException(string login)
			: base($"Не удалось найти пользователя с логином = {login}")
		{ }
	}
}
