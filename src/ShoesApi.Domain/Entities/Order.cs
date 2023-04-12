using ShoesApi.Domain.Common;

namespace ShoesApi.Domain.Entities;

/// <summary>
/// Заказ
/// </summary>
public class Order : EntityBase
{
	/// <summary>
	/// Дата заказа
	/// </summary>
	public DateTime OrderDate { get; set; }

	/// <summary>
	/// Адрес
	/// </summary>
	public string Address { get; set; } = default!;

	/// <summary>
	/// Итоговая сумма заказа
	/// </summary>
	public int Sum { get; set; }

	/// <summary>
	/// Количество вещей в заказе
	/// </summary>
	public int Count { get; set; }

	/// <summary>
	/// Идентификатор пользователя
	/// </summary>
	public int UserId { get; set; }


	#region navigation Properties

	/// <summary>
	/// Пользователь
	/// </summary>
	public User? User { get; set; }

	/// <summary>
	/// Части заказа
	/// </summary>
	public List<OrderItem>? OrderItems { get; set; }

	#endregion
}
