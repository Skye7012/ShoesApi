using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Entities;
using ShoesApi.Requests.OderdRequests;
using ShoesApi.Responses.OrderResponses.GetOrdersResponse;
using ShoesApi.Services;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly ShoesDbContext _context;
		private readonly IUserService _userService;

		public OrderController(
			ShoesDbContext context,
			IUserService userService)
		{
			_context = context;
			_userService = userService;
		}

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
					Sum = x.Sum,
					Count = x.Count,
					OrderItems = x.OrderItems!.Select(i => new GetOrdersResponseItemOrderItem()
						{
							Id = i.Id,
							RuSize = i.Size!.RuSize,
							Shoe = new GetOrdersResponseItemShoe()
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

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<int>> Post(OrderPostRequest request)
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
				Count = orderItems.Count(),
				Sum = orderItems.Sum(x => x.Shoe!.Price),
				User = user,
				OrderItems = orderItems,
			};

			await _context.AddAsync(order);
			await _context.SaveChangesAsync();

			return Ok(order.Id);
		}

		private bool isOrderItemsUnique(OrderPostRequest request)
		{
			return request.OrderItems
				.GroupBy(x => new { x.ShoeId, x.RuSize })
				.All(x => x.Count() == 1);
		}
	}
}
