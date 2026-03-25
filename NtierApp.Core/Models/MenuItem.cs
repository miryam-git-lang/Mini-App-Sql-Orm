using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.Core.Models
{
	public class MenuItem
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Name { get; set; }
		public decimal Price { get; set; }
		public List<OrderItem> OrderItems { get; set; }
        public Enum.Category Category { get; set; }


    }
}
