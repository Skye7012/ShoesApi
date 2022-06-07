using System.Security.Claims;

namespace ShoesApi.Services
{
	/// <inheritdoc/>
	public class UserService : IUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="httpContextAccessor">Accessor to http context</param>
		public UserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		/// <inheritdoc/>
		public string GetLogin()
		{
			var result = string.Empty;
			if (_httpContextAccessor.HttpContext != null)
			{
				result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
			}
			return result;
		}
	}
}
