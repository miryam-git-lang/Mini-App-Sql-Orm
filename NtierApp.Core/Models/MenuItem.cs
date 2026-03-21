using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.Core.Models
{
	public class MenuItem
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public decimal Price { get; set; }
		public Enum.Category Category { get; set; }
		public OrderItem? OrderItem { get; set; }

	}
}
