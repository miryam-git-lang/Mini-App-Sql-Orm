using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NtierApp.BLL.Interfaces;
using NtierApp.Core.Models;
using NtierApp.DAL.Context;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NtierApp.BLL.Services
{
	public class OrderService : IOrder
	{
		AppDbContext ntierDbContext = new AppDbContext();

		public async Task<List<Order>> Orders()
		{
			return await ntierDbContext.Orders
				.Include(o => o.OrderItems) 
				.ThenInclude(oi => oi.MenuItem)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Order> AddOrder()
		{
			throw new NotImplementedException();
		}

		public async Task<List<Order>> GetOrderByDate(DateTime date)
		{
			var orders = await ntierDbContext.Orders
				.Where(o => o.Date == date)
				.Include(o => o.OrderItems)
				.AsNoTracking()
				.ToListAsync();

			return orders;
		}

		public async Task<List<Order>> GetOrderByNo(Guid No)
		{
			var orders = await ntierDbContext.Orders
				.Include(o => o.OrderItems)
				.ThenInclude(oi => oi.MenuItem)
				.FirstOrDefaultAsync(o => o.Id == No);

			return orders;
		}

		public async Task<List<Order>> GetOrdersByDatesInterval(DateTime date1, DateTime date2)
		{
			var orders = await ntierDbContext.Orders
				.Where(o => o.Date >= date1 && o.Date <= date2)
				.AsNoTracking()
				.ToListAsync();

			return orders;
		}

		public async Task<List<Order>> GetOrdersByPriceInterval(decimal minPrice, decimal maxPrice)
		{
			var orders = await ntierDbContext.Orders
				.Where(o => o.TotalAmount >= minPrice && o.TotalAmount <= maxPrice)
				.AsNoTracking()
				.ToListAsync();

			return orders;
		}

		
		public async Task<Order> RemoveOrder(Guid id)
		{
			var existingOrder = await ntierDbContext.Orders.FirstOrDefaultAsync(m => m.Id == id);
			if (existingOrder == null)
				throw new ArgumentException(nameof(id), $"Order with id {id} not found");
			else
			{
				ntierDbContext.Orders.Remove(existingOrder);
				await ntierDbContext.SaveChangesAsync();
				return existingOrder;
			}
		}
	}
}
