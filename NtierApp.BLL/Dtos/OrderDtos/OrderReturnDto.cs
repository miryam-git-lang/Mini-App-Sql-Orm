using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierApp.BLL.Dtos.OrderItemsDto;

namespace NtierApp.BLL.Dtos.OrderDtos
{
	public class OrderReturnDto
	{
		public Guid Id { get; set; }
		public decimal TotalAmount { get; set; }
		public DateTime Date { get; set; }

		public List<OrderItemReturnDto> Items { get; set; } = null!;
	}
}
