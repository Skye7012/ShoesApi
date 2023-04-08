using System.Net.Http;
using System.Threading.Tasks;

namespace ShoesApi.IntegrationTests
{
	/// <summary>
	/// Расширения для <see cref="HttpClient"/>
	/// </summary>
	public static class HttpClientExtensions
	{
		/// <summary>
		/// Получить ответ приведенный к типу T
		/// </summary>
		/// <typeparam name="T">Тип ответа</typeparam>
		/// <param name="response">HTTP ответ</param>
		public static async Task<T> GetResponseAsyncAs<T>(this Task<HttpResponseMessage> response)
			=> await (await response).Content.ReadAsAsync<T>();
	}
}
