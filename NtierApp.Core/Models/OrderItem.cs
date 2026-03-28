using NtierApp.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.Core.Models
{
	public class OrderItem : BaseEntity
    {
		public Guid MenuItemId { get; set; }
		public MenuItem MenuItem { get; set; } = null!;

		public Guid OrderId { get; set; }
		public Order Order { get; set; } = null!;

		public int Count { get; set; }
	}
}
