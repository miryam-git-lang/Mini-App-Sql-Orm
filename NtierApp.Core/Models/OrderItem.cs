using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.Core.Models
{
	public class OrderItem
	{
		public Guid id { get; set; }
		public int Count { get; set; }
		public int MenuItemId { get; set; }
		public MenuItem MenuItem { get; set; } = null!;
		public int OrderId { get; set; }
		public Order Order { get; set; } = null!;


	}
}
