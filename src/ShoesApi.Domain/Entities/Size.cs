using ShoesApi.Domain.Common;

namespace ShoesApi.Domain.Entities;

/// <summary>
/// Размер обуви
/// </summary>
public class Size : EntityBase
{
	/// <summary>
	/// Российский размер
	/// </summary>
	public int RuSize { get; set; }

	#region navigation Properties

	/// <summary>
	/// Кроссовки
	/// </summary>
	public List<Shoe>? Shoes { get; private set; }

	/// <summary>
	/// Части заказа
	/// </summary>
	public List<OrderItem>? OrderItems { get; private set; }

	#endregion
}
