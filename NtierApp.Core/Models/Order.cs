using NtierApp.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.Core.Models
{
	public class Order : BaseEntity
    {
		public List<OrderItem> OrderItems { get; set; } = null!;
		public decimal TotalAmount { get; set; }
		public DateTime Date { get; set; }
	}
}
