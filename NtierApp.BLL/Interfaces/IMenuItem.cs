using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierApp.Core.Models;
namespace NtierApp.BLL.Interfaces
{
	public interface IMenuItem
	{
		Task<List<MenuItem>> MenuItems();
		Task<MenuItem> AddMenuItem(MenuItem menuItem);
		Task<MenuItem> RemoveMenuItem(Guid id);
		Task<MenuItem> EditMenuItem(Guid id, MenuItem menuItem);
		Task<List<MenuItem>> GetByCategory(string category);
		Task<List<MenuItem>> GetByPriceInterval(decimal minPrice, decimal maxPrice);
		Task<List<MenuItem>> GetByName(string name);

	}
}
