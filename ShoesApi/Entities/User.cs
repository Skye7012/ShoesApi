namespace ShoesApi.Entities
{
	/// <summary>
	/// Пользователь
	/// </summary>
	public class User : EntityBase
	{
		/// <summary>
		/// Login
		/// </summary>
		public string Login { get; set; } = null!;

		/// <summary>
		/// PasswordHash
		/// </summary>
		public byte[] PasswordHash { get; set; } = null!;

		/// <summary>
		/// PasswordSalt
		/// </summary>
		public byte[] PasswordSalt { get; set; } = null!;

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; } = null!;

		/// <summary>
		/// FirstName
		/// </summary>
		public string? FirstName { get; set; }

		/// <summary>
		/// Phone
		/// </summary>
		public string? Phone { get; set; }

		#region navigation Properties

		/// <summary>
		/// Orders
		/// </summary>
		public List<Order>? Orders { get; set; }

		#endregion

	}
}
