using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Contracts.Requests.OrderRequests.GetOrdersResponse;
using ShoesApi.Contracts.Requests.OrderRequests.PostOrderRequest;
using ShoesApi.Entities;
using ShoesApi.Services;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Controllers
{
	/// <summary>
	/// Orders Controller
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class OrdersController : ControllerBase
	{
		private readonly ShoesDbContext _context;
		private readonly IUserService _userService;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context">DbContext</param>
		/// <param name="userService">User service</param>
		public OrdersController(
			ShoesDbContext context,
			IUserService userService)
		{
			_context = context;
			_userService = userService;
		}

		/// <summary>
		/// Get Orders
		/// </summary>
		/// <returns>Orders</returns>
		[HttpGet]
		[Authorize]
		public async Task<GetOrdersResponse> Get()
		{
			var login = _userService.GetLogin();
			var user = await _context.Users
				.FirstOrDefaultAsync(x => x.Login == login)
				?? throw new Exception("User not found");

			var query = _context.Orders
				.Where(x => x.User == user)
				.Select(x => new GetOrdersResponseItem()
				{
					Id = x.Id,
					OrderDate = x.OrderDate,
					Address = x.Address,
					Sum = x.Sum,
					Count = x.Count,
					OrderItems = x.OrderItems!.Select(i => new GetOrdersResponseItemOrderItem()
						{
							Id = i.Id,
							RuSize = i.Size!.RuSize,
							Shoe = new GetOrdersResponseItemOrderItemShoe()
							{
								Id = i.Shoe!.Id,
								Image = i.Shoe!.Image,
								Name = i.Shoe!.Name,
								Price = i.Shoe!.Price,
							}
						})
						.ToList()
				});

			var orders = await query.ToListAsync();
			var count = await query.CountAsync();

			return new GetOrdersResponse()
			{
				Items = orders,
				TotalCount = count,
			};
		}

		/// <summary>
		/// Post Orders
		/// </summary>
		/// <param name="request">Request</param>
		/// <returns>Order Id</returns>
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<int>> Post(PostOrderRequest request)
		{
			var login = _userService.GetLogin();
			var user = await _context.Users
				.FirstOrDefaultAsync(x => x.Login == login)
				?? throw new Exception("User not found");

			if (!isOrderItemsUnique(request))
				throw new Exception("ShoeId and RuSize combination must be unique");

			var shoes = await _context.Shoes
				.Where(x => request.OrderItems.Select(x => x.ShoeId).Contains(x.Id))
				.ToListAsync();

			var sizes = await _context.Sizes
				.Where(x => request.OrderItems.Select(x => x.RuSize).Contains(x.RuSize))
				.ToListAsync();

			var orderItems = request.OrderItems
				.Select(x => new OrderItem()
				{
					Shoe = shoes.FirstOrDefault(s => s.Id == x.ShoeId)
						?? throw new Exception($"Not found shoe with id {x.ShoeId}"),
					Size = sizes.FirstOrDefault(s => s.RuSize == x.RuSize)
						?? throw new Exception($"Not found RuSuze {x.RuSize}"),
				})
				.ToList();

			var order = new Order()
			{
				OrderDate = DateTime.UtcNow,
				Address = request.Address,
				Count = orderItems.Count(),
				Sum = orderItems.Sum(x => x.Shoe!.Price),
				User = user,
				OrderItems = orderItems,
			};

			await _context.AddAsync(order);
			await _context.SaveChangesAsync();

			return Ok(order.Id);
		}

		private bool isOrderItemsUnique(PostOrderRequest request)
		{
			return request.OrderItems
				.GroupBy(x => new { x.ShoeId, x.RuSize })
				.All(x => x.Count() == 1);
		}
	}
}
