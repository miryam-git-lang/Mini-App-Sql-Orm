using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.Core.Models
{
	public class Category
	{
		public Guid id { get; set; }
		public string Name { get; set; }
		public List<MenuItem> MenuItems { get; set; }

	}
}
