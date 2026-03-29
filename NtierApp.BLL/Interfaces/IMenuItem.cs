using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierApp.BLL.Dtos.MenuItemsDtos;
using NtierApp.Core.Models;
using static NtierApp.Core.Models.Enum;
namespace NtierApp.BLL.Interfaces
{
	public interface IMenuItem
	{
		Task<List<MenuItemReturnDto>> MenuItems();
		Task AddMenuItem(MenuItemCreateDto menuItemCreateDto);
		Task<MenuItemReturnDto> RemoveMenuItem(Guid id);
		Task<MenuItemReturnDto> EditMenuItem(Guid id, MenuItemCreateDto menuItemCreateDto);
		Task<List<MenuItemReturnDto>> GetByCategory(Category category);
		Task<List<MenuItemReturnDto>> GetByPriceInterval(decimal minPrice, decimal maxPrice);
		Task<List<MenuItemReturnDto>> GetByName(string name);

	}
}
