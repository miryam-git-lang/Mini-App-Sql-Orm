using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.Core.Models
{
	public class OrderItem
	{
		public Guid Id { get; set; }

		public Guid MenuItemId { get; set; }
		public MenuItem MenuItem { get; set; }

		public Guid OrderId { get; set; }
		public Order Order { get; set; }

		public int Count { get; set; }
	}
}
