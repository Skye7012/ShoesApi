namespace ShoesApi.CQRS.Commands.OrderCommands.PostOrder
{
	/// <summary>
	/// ДТО Части заказа из <see cref="PostOrderCommand"/>
	/// </summary>
	public class PostOrderCommandOrderItem
	{
		/// <summary>
		/// Идентификатор обуви
		/// </summary>
		public int ShoeId { get; set; }

		/// <summary>
		/// Выбранный Российский размер
		/// </summary>
		public int RuSize { get; set; }
	}
}
