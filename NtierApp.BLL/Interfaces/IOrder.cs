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
		Task<Order> AddOrder();
		Task<Order> RemoveOrder(Guid id);
		Task<List<Order>> GetOrdersByDatesInterval(DateTime date1, DateTime date2);
		Task<List<Order>> GetOrderByDate(DateTime date);
		Task<List<Order>> GetOrdersByPriceInterval(decimal min,decimal max);
		Task<List<Order>> GetOrderByNo(Guid No);

	}
}
