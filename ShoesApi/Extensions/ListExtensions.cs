using ShoesApi.Entities;

namespace ShoesApi.Extensions
{
	/// <summary>
	/// Расширения для <see cref="List{T}"/>
	/// </summary>
	public static class ListExtensions
	{
		/// <summary>
		/// Обновить связь многие ко многим <br/>
		/// Просто удалить записи из исходной коллекции, которые не попали в новый список<br/>
		/// И добавить записи из новой коллекции, которых не было в исходной коллекции<br/>
		/// </summary>
		/// <typeparam name="T">Тип элемента</typeparam>
		/// <param name="source">Исходная коллекция</param>
		/// <param name="updated">Обновленная коллекция</param>
		public static void UpdateManyToManyRelation<T>(this List<T> source, List<T> updated) where T : EntityBase
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			var sameItems = source.Intersect(updated);
			source.RemoveRange(source.Except(sameItems));
			source.AddRange(updated.Except(sameItems));
		}

		/// <summary>
		/// Удалить элементы из коллекции
		/// </summary>
		/// <typeparam name="T">Тип элемента</typeparam>
		/// <param name="source">Исходная коллекция</param>
		/// <param name="collection">Элементы для удаления</param>
		public static void RemoveRange<T>(this List<T> source, IEnumerable<T> collection)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			foreach (var item in collection)
				source.Remove(item);
		}
	}
}
