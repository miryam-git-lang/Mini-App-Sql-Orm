using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NtierApp.BLL.Interfaces;
using NtierApp.Core.Models;
using NtierApp.DAL.Context;
using NtierApp.DAL.Interfaces;

namespace NtierApp.BLL.Services
{
	public class MenuItemService : IMenuItem
	{
		private readonly IRepository<MenuItem> repository;	
		public MenuItemService(IRepository<MenuItem> repository)
		{
			this.repository = repository;
		}
		public async Task<List<MenuItem>> MenuItems()
		{
			return await repository.GetAll().ToListAsync();
		}

		public async Task AddMenuItem(MenuItem menuItem)
		{
			if (string.IsNullOrWhiteSpace(menuItem.Name))
				throw new ArgumentException(nameof(menuItem.Name), "Menu item cannot be empty");

			if (await repository.IsExistAsync(n => n.Name.ToLower() == menuItem.Name.ToLower()))
				throw new ArgumentException(nameof(menuItem.Name), "This item is already exists");

			if (menuItem.Price <= 0)
				throw new ArgumentException(nameof(menuItem.Price), "Price must be greater than zero");

			await repository.AddAsync(menuItem);
			await repository.SaveChangesAsync();
		}

		public async Task<MenuItem> EditMenuItem(Guid id, MenuItem menuItem)
		{
			var existingMenuItem = await repository.GetByIdAsync(id);
			if (existingMenuItem == null)
				throw new ArgumentException(nameof(id), $"Menu item with id {id} not found");

			if(string.IsNullOrWhiteSpace(menuItem.Name))
				throw new ArgumentException(nameof(menuItem.Name), "Menu item cannot be empty");

			if (menuItem.Price <= 0)
				throw new ArgumentException(nameof(menuItem.Price), "Price must be greater than zero");
			else
			{
				existingMenuItem.Name = menuItem.Name;
				existingMenuItem.Price = menuItem.Price;
				repository.Update(existingMenuItem);
				await repository.SaveChangesAsync();
				return existingMenuItem;
			}

		}

		public async Task<MenuItem> RemoveMenuItem(Guid id)
		{
			var existingMenuItem = await repository.GetByIdAsync(id);
			if (existingMenuItem == null)
				throw new ArgumentException(nameof(id), $"Menu item with id {id} not found");
			else
			{
				repository.Delete(existingMenuItem);
				await repository.SaveChangesAsync();
				return existingMenuItem;
			}
		}

		public async Task<List<MenuItem>> GetByCategory(NtierApp.Core.Models.Enum.Category category)
		{
			var menuItems = await repository.GetAll(false, m => m.Category == category, 1,2).ToListAsync();	

			return menuItems;
		}
		public async Task<List<MenuItem>> GetByName(string name)
		{
			var menuItems = await repository.GetAll(false, m => m.Name == name, 1,2).ToListAsync();

			return menuItems;

		}
		public async Task<List<MenuItem>> GetByPriceInterval(decimal minPrice, decimal maxPrice)
		{
			var menuItems = await repository.GetAll(false, m => m.Price >= minPrice && m.Price <= maxPrice, 1,2).ToListAsync();

			if (menuItems.Count == 0)
				throw new Exception("Nothing found");

			return menuItems;
		}
	}
}
