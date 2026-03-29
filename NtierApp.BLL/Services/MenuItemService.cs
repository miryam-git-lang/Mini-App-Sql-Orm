using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NtierApp.BLL.Dtos.MenuItemsDtos;
using NtierApp.BLL.Interfaces;
using NtierApp.Core.Models;
using NtierApp.DAL.Context;
using NtierApp.DAL.Interfaces;

namespace NtierApp.BLL.Services
{
	public class MenuItemService(IRepository<MenuItem> repository, IMapper mapper) : IMenuItem
	{
		
		public async Task<List<MenuItemReturnDto>> MenuItems()
		{
			var menuItems = await repository.GetAll().ToListAsync();
			return mapper.Map<List<MenuItemReturnDto>>(menuItems);
		}

		public async Task AddMenuItem(MenuItemCreateDto menuItemCreateDto)
		{
			if (string.IsNullOrWhiteSpace(menuItemCreateDto.Name))
				throw new ArgumentException(nameof(menuItemCreateDto.Name), "Menu item cannot be empty");

			if (await repository.IsExistAsync(n => n.Name.ToLower() == menuItemCreateDto.Name.ToLower()))
				throw new ArgumentException(nameof(menuItemCreateDto.Name), "This item is already exists");

			if (menuItemCreateDto.Price <= 0)
				throw new ArgumentException(nameof(menuItemCreateDto.Price), "Price must be greater than zero");

			MenuItem menuItem = mapper.Map<MenuItem>(menuItemCreateDto);

			await repository.AddAsync(menuItem);
			await repository.SaveChangesAsync();
		}

		public async Task<MenuItemReturnDto> EditMenuItem(Guid id, MenuItemCreateDto menuItemCreateDto)
		{
			var existingMenuItem = await repository.GetByIdAsync(id);
			if (existingMenuItem == null)
				throw new ArgumentException(nameof(id), $"Menu item with id {id} not found");

			if(string.IsNullOrWhiteSpace(menuItemCreateDto.Name))
				throw new ArgumentException(nameof(menuItemCreateDto.Name), "Menu item cannot be empty");

			if (menuItemCreateDto.Price <= 0)
				throw new ArgumentException(nameof(menuItemCreateDto.Price), "Price must be greater than zero");
			else
			{
				existingMenuItem.Name = menuItemCreateDto.Name;
				existingMenuItem.Price = menuItemCreateDto.Price;
				repository.Update(existingMenuItem);
				await repository.SaveChangesAsync();
				return mapper.Map<MenuItemReturnDto>(existingMenuItem);
			}

		}

		public async Task<MenuItemReturnDto> RemoveMenuItem(Guid id)
		{
			var existingMenuItem = await repository.GetByIdAsync(id);
			if (existingMenuItem == null)
				throw new ArgumentException(nameof(id), $"Menu item with id {id} not found");
			else
			{
				repository.Delete(existingMenuItem);
				await repository.SaveChangesAsync();
				return mapper.Map<MenuItemReturnDto>(existingMenuItem);
			}
		}

		public async Task<List<MenuItemReturnDto>> GetByCategory(NtierApp.Core.Models.Enum.Category category)
		{
			var menuItems = await repository.GetAll(false, m => m.Category == category, 1,2).ToListAsync();
			return mapper.Map<List<MenuItemReturnDto>>(menuItems);
		}
		public async Task<List<MenuItemReturnDto>> GetByName(string name)
		{
			var menuItems = await repository.GetAll(false, m => m.Name == name, 1,2).ToListAsync();
			return mapper.Map<List<MenuItemReturnDto>>(menuItems);

		}
		public async Task<List<MenuItemReturnDto>> GetByPriceInterval(decimal minPrice, decimal maxPrice)
		{
			var menuItems = await repository.GetAll(false, m => m.Price >= minPrice && m.Price <= maxPrice, 1,2).ToListAsync();

			if (menuItems.Count == 0)
				throw new Exception("Nothing found");
			return mapper.Map<List<MenuItemReturnDto>>(menuItems);
		}
	}
}
