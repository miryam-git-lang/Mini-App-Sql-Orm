using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NtierApp.BLL.Interfaces;
using NtierApp.Core.Models;
using NtierApp.DAL.Interfaces;

namespace NtierApp.BLL.Services
{
  public class OrderService : IOrder
	{
		private readonly IRepository<Order> orderRepository;

		public OrderService(IRepository<Order> orderRepository)
		{
			this.orderRepository = orderRepository;
		}

		public async Task<List<Order>> Orders()
		{
         return await orderRepository.GetAll(false, null, 1, 2, nameof(Order.OrderItems))
				.Include(o => o.OrderItems) 
				.ThenInclude(oi => oi.MenuItem)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Order> AddOrder(MenuItem menuItem, int count)
		{
          ArgumentNullException.ThrowIfNull(menuItem);
			if (count <= 0)
				throw new ArgumentOutOfRangeException(nameof(count), "Item count must be greater than zero.");

			var orderItem = new OrderItem
			{
				MenuItemId = menuItem.Id,
				Count = count
			};

			var order = new Order
			{
				Date = DateTime.UtcNow,
				TotalAmount = menuItem.Price * count,
				OrderItems = new List<OrderItem> { orderItem }
			};

			orderItem.Order = order;

			await orderRepository.AddAsync(order);
			await orderRepository.SaveChangesAsync();

			return order;
		}

        public async Task<List<Order>> GetOrderByDate(DateTime date)
		{
			var orders = await orderRepository.GetAll(false, o => o.Date == date, 1, 2, nameof(Order.OrderItems))
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.ToListAsync();

			return orders;
		}

		public async Task<Order> GetOrderByNo(Guid No)
		{
          var orders = await orderRepository.GetAll(false, o => o.Id == No, 1, 2, nameof(Order.OrderItems))
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.FirstOrDefaultAsync(o => o.Id == No);

			return orders;
		}

       public async Task<List<Order>> GetOrdersByDatesInterval(DateTime startDate, DateTime endDate)
		{
			var orders = await orderRepository.GetAll(false, o => o.Date >= startDate && o.Date <= endDate, 1, 2, nameof(Order.OrderItems))
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.ToListAsync();
			return orders;
		}

		public async Task<List<Order>> GetOrdersByPriceInterval(decimal minPrice, decimal maxPrice)
		{
         var orders = await orderRepository.GetAll(false, o => o.TotalAmount >= minPrice && o.TotalAmount <= maxPrice, 1, 2, nameof(Order.OrderItems))
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.ToListAsync();

			return orders;
		}

		
		public async Task<Order> RemoveOrder(Guid id)
		{
          var existingOrder = await orderRepository.GetByIdAsync(id);
			if (existingOrder == null)
				throw new ArgumentException(nameof(id), $"Order with id {id} not found");
			else
			{
               orderRepository.Delete(existingOrder);
				await orderRepository.SaveChangesAsync();
				return existingOrder;
			}
		}
	}
}
