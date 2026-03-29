using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.BLL.Dtos.OrderItemsDto
{
	public class OrderItemCreateDto
	{
		public Guid MenuItemId { get; set; }
		public int Count { get; set; }
	}
}
