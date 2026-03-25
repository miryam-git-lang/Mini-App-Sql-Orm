using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierApp.Core.Models;
using static NtierApp.Core.Models.Enum;
namespace NtierApp.BLL.Interfaces
{
	public interface IMenuItem
	{
		Task<List<MenuItem>> MenuItems();
		Task AddMenuItem(MenuItem menuItem);
		Task<MenuItem> RemoveMenuItem(Guid id);
		Task<MenuItem> EditMenuItem(Guid id, MenuItem menuItem);
		Task<List<MenuItem>> GetByCategory(Category category);
		Task<List<MenuItem>> GetByPriceInterval(decimal minPrice, decimal maxPrice);
		Task<List<MenuItem>> GetByName(string name);

	}
}
