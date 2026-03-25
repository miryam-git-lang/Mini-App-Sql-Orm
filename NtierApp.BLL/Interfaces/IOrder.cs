using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierApp.Core.Models;

namespace NtierApp.BLL.Interfaces
{
	public interface IOrder
	{
		Task<List<Order>> Orders();
		Task<Order> AddOrder(MenuItem menuItem, int count);
		Task<Order> RemoveOrder(Guid id);
		Task<List<Order>> GetOrdersByDatesInterval(DateTime startDate, DateTime endDate);
		Task<List<Order>> GetOrderByDate(DateTime date);
		Task<List<Order>> GetOrdersByPriceInterval(decimal min,decimal max);
		Task<Order> GetOrderByNo(Guid No);

	}
}
