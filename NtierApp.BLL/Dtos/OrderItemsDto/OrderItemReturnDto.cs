using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.BLL.Dtos.OrderItemsDto
{
	public class OrderItemReturnDto
	{
		public string MenuItemName { get; set; } = null!;
		public int Count { get; set; }
		public decimal Price { get; set; }
	}
}
