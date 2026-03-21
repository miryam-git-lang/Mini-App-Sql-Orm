using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.Core.Models
{
	public class Order
	{
		public Guid Id { get; set; }
		public decimal TotalAmount { get; set; }
		public DateTime Date { get; set; }
		public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

	}
}
