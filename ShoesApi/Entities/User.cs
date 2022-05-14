using System;
using System.Collections.Generic;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Пользователь
	/// </summary>
	public class User : EntityBase
	{
		public string Login { get; set; } = null!;

		public byte[] PasswordHash { get; set; } = null!;
		public byte[] PasswordSalt { get; set; } = null!;

	}
}
