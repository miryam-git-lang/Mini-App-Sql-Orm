using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierApp.BLL.Dtos.MenuItemsDtos;
using NtierApp.BLL.Dtos.OrderDtos;
using NtierApp.Core.Models;

namespace NtierApp.BLL.Interfaces
{
	public interface IOrder
	{
		Task<List<OrderReturnDto>> Orders();
		Task<Order> AddOrder(MenuItem menuItem, int count);
		Task<OrderReturnDto> RemoveOrder(Guid id);
		Task<List<OrderReturnDto>> GetOrdersByDatesInterval(DateTime startDate, DateTime endDate);
		Task<List<OrderReturnDto>> GetOrderByDate(DateTime date);
		Task<List<OrderReturnDto>> GetOrdersByPriceInterval(decimal min,decimal max);
		Task<OrderReturnDto?> GetOrderByNo(Guid No);

	}
}
