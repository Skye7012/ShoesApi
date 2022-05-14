namespace ShoesApi.Requests.UserRequests
{
	public class RegisterRequest
	{
		public string Login { get; set; } = default!;
		public string Password { get; set; } = default!;
	}
}
