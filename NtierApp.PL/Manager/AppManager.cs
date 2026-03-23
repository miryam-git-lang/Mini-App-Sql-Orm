using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierApp.BLL.Services;
using NtierApp.Core.Models;

namespace NtierApp.PL.Manager
{
	public class AppManager
	{
		private MenuItemService menuItemService = new MenuItemService();
		private OrderService orderService = new OrderService();

		public void Start()
		{
			while (true)
			{
				Console.WriteLine(" MAIN MENU ");
				Console.WriteLine("~~~~~~~~~~~~");
				Console.WriteLine("1 - Perform operations on the menu");
				Console.WriteLine("2 - Perform operations on the order");
				Console.WriteLine("0 - Exit");
				var input = int.Parse(Console.ReadLine());

				switch (input)
				{
					case 1:
					MenuService();
						break;

					case 2:
					OrderService();
						break;

					case 0:
						return;

				}


			}
		}

		private void MenuService()
		{
			Console.WriteLine(" MENU SERVICE ");
			Console.WriteLine("~~~~~~~~~~~~~~");
			Console.WriteLine("1 — Add new item");
			Console.WriteLine("2 — Edit item");
			Console.WriteLine("3 — Delete item");
			Console.WriteLine("4 — Show all items");
			Console.WriteLine("5 — Show by category");
			Console.WriteLine("6 — Show by price range");
			Console.WriteLine("7 — Search by name");
			Console.WriteLine("0 - Exit");
			var input = int.Parse(Console.ReadLine());

			if (input == 1)
			{
				Console.WriteLine("Enter item details");
				Console.WriteLine("Name:");
				var name = Console.ReadLine();
				Console.WriteLine("Price:");
				var price = decimal.Parse(Console.ReadLine());

				MenuItem menuItem = new MenuItem();

				string Name = name!;
				decimal Price = price;

				menuItem.Name = name!;
				menuItem.Price = price;

				MenuItemService menuItemService = new MenuItemService();

				menuItemService.AddMenuItem(menuItem);

			}

			if (input == 2)
			{
				MenuItem UpdatedMenuItem = new MenuItem();
				Console.WriteLine("Enter item details");
				Console.WriteLine("Id:");
				Guid id = Guid.Parse(Console.ReadLine());
				Console.WriteLine("Name:");
				UpdatedMenuItem.Name = Console.ReadLine();
				Console.WriteLine("Price:");
				UpdatedMenuItem.Price = int.Parse(Console.ReadLine());

				MenuItemService menuItemService = new MenuItemService();

				menuItemService.EditMenuItem(id, UpdatedMenuItem);

			}

			if (input == 3)
			{
				MenuItem UpdatedMenuItem = new MenuItem();
				Console.WriteLine("Enter item details");
				Console.WriteLine("Id:");
				Guid id = Guid.Parse(Console.ReadLine());

				MenuItemService menuItemService = new MenuItemService();

				menuItemService.RemoveMenuItem(id);

			}

			if (input == 4)
			{
				MenuItemService menuItemService = new MenuItemService();

				menuItemService.MenuItems();
			}

			if (input == 5)
			{

			}

			if (input == 6)
			{
				MenuItemService menuItemService = new MenuItemService();

				Console.WriteLine("Enter item details");
				Console.WriteLine("Min price:");
				var minPrice = int.Parse(Console.ReadLine());
				Console.WriteLine("Max price:");
				var maxPrice = int.Parse(Console.ReadLine());

				menuItemService.GetByPriceInterval(minPrice, maxPrice);
			}

			if (input == 7)
			{
				MenuItemService menuItemService = new MenuItemService();

				Console.WriteLine("Enter item details");
				Console.WriteLine("Name:");
				var Name = Console.ReadLine();

				menuItemService.GetByName(Name);
			}

			if (input == 0)
			{
				return;
			}
		}

		private void OrderService()
		{
			Console.WriteLine(" ORDER SERVICE ");
			Console.WriteLine("~~~~~~~~~~~~~~");
			Console.WriteLine("1 - Add new order");
			Console.WriteLine("2 - Delete order");
			Console.WriteLine("3 - Show all orders");
			Console.WriteLine("4 — Orders by date range");
			Console.WriteLine("5 — Orders by amount range");
			Console.WriteLine("6 — Orders for a specific date");
			Console.WriteLine("7 — Order details by ID");
			Console.WriteLine("0 - Exit");
			var input = int.Parse(Console.ReadLine());

		}

		
	}
}
