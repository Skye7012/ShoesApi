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

		public string Name { get; set; } = null!;

		public string? Fname { get; set; }

		public string? Phone { get; set; }

		#region navigation Properties

		public List<Order>? Orders { get; set; }

		#endregion

	}
}
