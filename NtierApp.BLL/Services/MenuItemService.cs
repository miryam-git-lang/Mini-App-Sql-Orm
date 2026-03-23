using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NtierApp.BLL.Interfaces;
using NtierApp.Core.Models;
using NtierApp.DAL.Context;
using static NtierApp.Core.Models.Enum;

namespace NtierApp.BLL.Services
{
	public class MenuItemService() : IMenuItem
	{
		Context ntierDbContext = new Context();
		public async Task<List<MenuItem>> MenuItems()
		{
			return await ntierDbContext.MenuItems
			.AsNoTracking() 
			.ToListAsync();
		}

		public async Task<MenuItem> AddMenuItem(MenuItem menuItem)
		{
			if (string.IsNullOrWhiteSpace(menuItem.Name))
				throw new ArgumentException(nameof(menuItem.Name), "Menu item cannot be empty");

			if(await ntierDbContext.MenuItems.AnyAsync(n => n.Name.ToLower() == menuItem.Name.ToLower()))
				throw new ArgumentException(nameof(menuItem.Name), "This item is already exists");

			if (menuItem.Price <= 0)
				throw new ArgumentException(nameof(menuItem.Price), "Price must be greater than zero");

			ntierDbContext.MenuItems.Add(menuItem);
			await ntierDbContext.SaveChangesAsync();
			return menuItem;
		}

		public async Task<MenuItem> EditMenuItem(Guid id, MenuItem menuItem)
		{
			var existingMenuItem = await ntierDbContext.MenuItems.FirstOrDefaultAsync(m => m.Id == id);
			if(existingMenuItem == null)
				throw new ArgumentException(nameof(id), $"Menu item with id {id} not found");

			if(string.IsNullOrWhiteSpace(menuItem.Name))
				throw new ArgumentException(nameof(menuItem.Name), "Menu item cannot be empty");

			if (menuItem.Price <= 0)
				throw new ArgumentException(nameof(menuItem.Price), "Price must be greater than zero");
			else
			{
				existingMenuItem.Name = menuItem.Name;
				existingMenuItem.Price = menuItem.Price;
				existingMenuItem.Category = menuItem.Category;
				await ntierDbContext.SaveChangesAsync();
				return existingMenuItem;
			}

		}

		public async Task<MenuItem> RemoveMenuItem(Guid id)
		{
			var existingMenuItem = await ntierDbContext.MenuItems.FirstOrDefaultAsync(m => m.Id == id);
			if (existingMenuItem == null)
				throw new ArgumentException(nameof(id), $"Menu item with id {id} not found");
			else
			{
				ntierDbContext.MenuItems.Remove(existingMenuItem);
				await ntierDbContext.SaveChangesAsync();
				return existingMenuItem;
			}
		}

		public async Task<MenuItem> GetByCategory(Category category)
		{
			throw new NotImplementedException();
		}
		public async Task<MenuItem> GetByName(string name)
		{
			var existingMenuItemName = await ntierDbContext.MenuItems.FirstOrDefaultAsync(m => m.Name == name);
			if (existingMenuItemName == null)
				throw new ArgumentException(nameof(name), $"Menu item with name: {name} not found");
			else
				return existingMenuItemName;

		}
		public async Task<MenuItem> GetByPriceInterval(decimal minPrice, decimal maxPrice)
		{
			throw new NotImplementedException();
		}
	
	}
}
