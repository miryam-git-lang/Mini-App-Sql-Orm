using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NtierApp.BLL.Interfaces;
using NtierApp.Core.Models;
using NtierApp.DAL.Context;
using NtierApp.DAL.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NtierApp.BLL.Services
{
	public class OrderService : IOrder
	{

		private readonly IRepository<Order> repository;
		private readonly IRepository<OrderItem> orderItemRepository;
		private IRepository<Order> orderRepository;

		public OrderService(IRepository<Order> repository, IRepository<OrderItem> orderItemRepository)
		{
			this.repository = repository;
			this.orderItemRepository = orderItemRepository;
		}

		public OrderService(IRepository<Order> orderRepository)
		{
			this.orderRepository = orderRepository;
		}

		public async Task<List<Order>> Orders()
		{
			return await repository.GetAll(false,null,1,2,"OrderItems")
				.Include(o => o.OrderItems) 
				.ThenInclude(oi => oi.MenuItem)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Order> AddOrder(MenuItem menuItem, int count)
		{
			Order order = new Order();
			OrderItem orderItem = new OrderItem();
			order.Date = DateTime.Now;
			order.TotalAmount += menuItem.Price * count;

			orderItem.MenuItemId = menuItem.Id;
			orderItem.Count = count;

			orderItem.Order = order;
		
			repository.AddAsync(order);
			await repository.SaveChangesAsync();

			return order;
		}

		public async Task<List<Order>> GetOrderByDate(DateTime date)
		{
			var orders = await repository.GetAll(false, o => o.Date == date, 1,2,"OrderItems").ToListAsync();

			return orders;
		}

		public async Task<Order> GetOrderByNo(Guid No)
		{
			var orders = await repository.GetAll(false, o => o.Id == No, 1,2,"OrderItems")
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.FirstOrDefaultAsync(o => o.Id == No);

			return orders;
		}

		public async Task<List<Order>> GetOrdersByDatesInterval(DateTime startDate, DateTime endDate)
		{
			var orders = await repository.GetAll(false, o => o.Date >= startDate && o.Date <= endDate, 1,2,"OrderItems").ToListAsync();
			return orders;
		}

		public async Task<List<Order>> GetOrdersByPriceInterval(decimal minPrice, decimal maxPrice)
		{
			var orders = await repository.GetAll(false, o => o.TotalAmount >= minPrice && o.TotalAmount <= maxPrice).ToListAsync();

			return orders;
		}

		
		public async Task<Order> RemoveOrder(Guid id)
		{
			var existingOrder = await repository.GetByIdAsync(id);
			if (existingOrder == null)
				throw new ArgumentException(nameof(id), $"Order with id {id} not found");
			else
			{
				repository.Delete(existingOrder);
				await repository.SaveChangesAsync();
				return existingOrder;
			}
		}
	}
}
