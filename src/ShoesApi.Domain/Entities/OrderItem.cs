using ShoesApi.Domain.Common;

namespace ShoesApi.Domain.Entities;

/// <summary>
/// Часть заказа
/// </summary>
public class OrderItem : EntityBase
{
	/// <summary>
	/// Идентификатор заказа
	/// </summary>
	public int OrderId { get; set; }

	/// <summary>
	/// Идентификатор обуви
	/// </summary>
	public int ShoeId { get; set; }

	/// <summary>
	/// Идентификатор Размера обуви
	/// </summary>
	public int SizeId { get; set; }

	#region navigation Properties

	/// <summary>
	/// Заказ
	/// </summary>
	public Order? Order { get; set; }

	/// <summary>
	/// Обувь
	/// </summary>
	public Shoe? Shoe { get; set; }

	/// <summary>
	/// Размер обуви
	/// </summary>
	public Size? Size { get; set; }

	#endregion
}
