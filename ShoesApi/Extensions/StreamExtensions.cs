namespace ShoesApi.Extensions
{
	/// <summary>
	/// Расширение для <see cref="Stream"/>
	/// </summary>
	public static class StreamExtensions
	{
		/// <summary>
		/// Получить поток
		/// </summary>
		public static Stream GetStream(this IFormFile source)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			var stream = new MemoryStream();
			source.CopyTo(stream);
			stream.Position = 0;

			return stream;
		}

		/// <summary>
		/// Являются ли потоки одинаковыми
		/// </summary>
		/// <param name="self">Сравниваемый поток</param>
		/// <param name="other">Поток с которым сравнивают</param>
		/// <returns>Являются ли потоки одинаковыми</returns>
		public static bool StreamEquals(this Stream self, Stream other)
		{
			if (self == other)
			{
				return true;
			}

			if (self == null || other == null)
			{
				throw new ArgumentNullException(self == null ? "self" : "other");
			}

			if (self.Length != other.Length)
			{
				return false;
			}

			for (int i = 0; i < self.Length; i++)
			{
				int aByte = self.ReadByte();
				int bByte = other.ReadByte();
				if (aByte.CompareTo(bByte) != 0)
				{
					return false;
				}
			}

			return true;
		}
	}
}
