using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NtierApp.Core.Models.Enum;

namespace NtierApp.BLL.Dtos.MenuItemsDtos
{
	public class MenuItemReturnDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public decimal Price { get; set; }
		public Category Category { get; set; }
	}
}
