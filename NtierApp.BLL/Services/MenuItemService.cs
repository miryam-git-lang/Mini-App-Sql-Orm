using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NtierApp.BLL.Interfaces;
using NtierApp.Core.Models;
using NtierApp.DAL.Context;

namespace NtierApp.BLL.Services
{
	public class MenuItemService() : IMenuItem
	{
		AppDbContext ntierDbContext = new AppDbContext();
		public async Task<List<MenuItem>> MenuItems()
		{
			return await ntierDbContext.MenuItems
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task AddMenuItem(MenuItem menuItem)
		{

			if (string.IsNullOrWhiteSpace(menuItem.Name))
				throw new ArgumentException(nameof(menuItem.Name), "Menu item cannot be empty");

			if (await ntierDbContext.MenuItems.AnyAsync(n => n.Name.ToLower() == menuItem.Name.ToLower()))
				throw new ArgumentException(nameof(menuItem.Name), "This item is already exists");

			if (menuItem.Price <= 0)
				throw new ArgumentException(nameof(menuItem.Price), "Price must be greater than zero");

			ntierDbContext.MenuItems.Add(menuItem);
			await ntierDbContext.SaveChangesAsync();
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

		public async Task<List<MenuItem>> GetByCategory(NtierApp.Core.Models.Enum.Category category)
		{
			var menuItems = await ntierDbContext.MenuItems
				.Where(m => m.Category == category)
				.AsNoTracking()
				.ToListAsync();

			return menuItems;
		}
		public async Task<List<MenuItem>> GetByName(string name)
		{
			var menuItems = await ntierDbContext.MenuItems
				.Where(m => m.Name == name)
				.AsNoTracking()
				.ToListAsync();

			return menuItems;

		}
		public async Task<List<MenuItem>> GetByPriceInterval(decimal minPrice, decimal maxPrice)
		{
			var menuItems = await ntierDbContext.MenuItems
				.Where(m => m.Price >= minPrice && m.Price <= maxPrice)
				.AsNoTracking()
				.ToListAsync();
				
			if (menuItems.Count == 0)
				throw new Exception("Nothing found");

			return menuItems;
		}

	}
}
