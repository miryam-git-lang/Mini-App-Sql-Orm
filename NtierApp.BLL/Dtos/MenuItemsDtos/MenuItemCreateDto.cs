using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierApp.Core.Models;
using static NtierApp.Core.Models.Enum;

namespace NtierApp.BLL.Dtos.MenuItemsDtos
{
	public class MenuItemCreateDto
	{
		public string Name { get; set; } = null!;
		public decimal Price { get; set; }
		public Category Category { get; set; }
	}
	
}
