using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NtierApp.BLL.Dtos.MenuItemsDtos;
using NtierApp.BLL.Dtos.OrderDtos;
using NtierApp.BLL.Interfaces;
using NtierApp.Core.Models;
using NtierApp.DAL.Interfaces;

namespace NtierApp.BLL.Services
{
  public class OrderService(IRepository<Order> orderRepository, IMapper mapper) : IOrder
	{

		public async Task<List<OrderReturnDto>> Orders()
		{
			var orders = await orderRepository.GetAll(false, null, 1, 2, "OrderItems")
				.Include(oi => oi.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.AsNoTracking()
				.ToListAsync();
			return mapper.Map<List<OrderReturnDto>>(orders);
		}

		public async Task<Order> AddOrder(MenuItem menuItem, int count)
		{
			if (menuItem == null)
				throw new ArgumentException(nameof(menuItem), "Menu item cannot be empty");
			if (count <= 0)
				throw new ArgumentException(nameof(count), "Item count must be greater than zero.");

			var orderItem = new OrderItem
			{
				MenuItemId = menuItem.Id,
				Count = count
			};

			var order = new Order
			{
				Date = DateTime.Now,
				TotalAmount = menuItem.Price * count,
				OrderItems = new List<OrderItem> { orderItem }
			};

			orderItem.Order = order;

			await orderRepository.AddAsync(order);
			await orderRepository.SaveChangesAsync();

			return order;
		}

        public async Task<List<OrderReturnDto>> GetOrderByDate(DateTime date)
		{
			var orders = await orderRepository.GetAll(false, o => o.Date == date, 1, 2, "OrderItems")
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.ToListAsync();

			return mapper.Map<List<OrderReturnDto>>(orders);
		}

		public async Task<OrderReturnDto?> GetOrderByNo(Guid No)
		{
          var existingOrder = await orderRepository.GetAll(false, o => o.Id == No, 1, 2, "OrderItems")
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.FirstOrDefaultAsync(o => o.Id == No);

			return mapper.Map<OrderReturnDto>(existingOrder);
		}

       public async Task<List<OrderReturnDto>> GetOrdersByDatesInterval(DateTime startDate, DateTime endDate)
	   {
			var orders = await orderRepository.GetAll(false, o => o.Date >= startDate && o.Date <= endDate, 1, 2, nameof(Order.OrderItems))
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.ToListAsync();

			return mapper.Map<List<OrderReturnDto>>(orders);
	   }

		public async Task<List<OrderReturnDto>> GetOrdersByPriceInterval(decimal minPrice, decimal maxPrice)
		{
			var orders = await orderRepository.GetAll(false, o => o.TotalAmount >= minPrice && o.TotalAmount <= maxPrice, 1, 2, nameof(Order.OrderItems))
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.ToListAsync();

			return mapper.Map<List<OrderReturnDto>>(orders);
		}

		
		public async Task<OrderReturnDto> RemoveOrder(Guid id)
		{
			var existingOrder = await orderRepository.GetByIdAsync(id);
			if (existingOrder == null)
				throw new ArgumentException(nameof(id), $"Order with id {id} not found");
			else
			{
                orderRepository.Delete(existingOrder);
				await orderRepository.SaveChangesAsync();
				return mapper.Map<OrderReturnDto>(existingOrder);
			}
		}
	}
}
