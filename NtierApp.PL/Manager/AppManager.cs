using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierApp.BLL.Services;
using NtierApp.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

		private async Task MenuService()
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
				Console.WriteLine("Category:");
				var category = Console.ReadLine();

				MenuItem menuItem = new MenuItem();

				menuItem.Name = name!;
				menuItem.Price = price;
				menuItem.Category.Name = category!;

				MenuItemService menuItemService = new MenuItemService();

				await menuItemService.AddMenuItem(menuItem);

			}

			else if (input == 2)
			{
				MenuItem UpdatedMenuItem = new MenuItem();
				Console.WriteLine("Enter item details");
				Console.WriteLine("Id:");
				Guid id = Guid.Parse(Console.ReadLine());
				Console.WriteLine("Choose what to change");
				Console.WriteLine("1 - Name");
				Console.WriteLine("2 - Category");
				Console.WriteLine("3 - Price");
				var input1 = int.Parse(Console.ReadLine());
				if (input1 == 1)
				{
					Console.WriteLine("Name:");
					UpdatedMenuItem.Name = Console.ReadLine();
				}

				else if (input1 == 2)
				{
					Console.WriteLine("Category:");
					UpdatedMenuItem.Category.Name = Console.ReadLine();
				}

				else if (input1 == 2)
				{
					Console.WriteLine("Price:");
					UpdatedMenuItem.Price = int.Parse(Console.ReadLine());
				}	

				MenuItemService menuItemService = new MenuItemService();

				await menuItemService.EditMenuItem(id, UpdatedMenuItem);

			}

			else if (input == 3)
			{
				MenuItem UpdatedMenuItem = new MenuItem();
				Console.WriteLine("Enter item details");
				Console.WriteLine("Id:");
				Guid id = Guid.Parse(Console.ReadLine());

				MenuItemService menuItemService = new MenuItemService();

				await menuItemService.RemoveMenuItem(id);

			}

			else if (input == 4)
			{
				MenuItemService menuItemService = new MenuItemService();

				var items = await menuItemService.MenuItems();

				foreach (var item in items)
					Console.WriteLine($"{item.Id} {item.Name} {item.Price}");
			}

			else if (input == 5)
			{
				MenuItemService menuItemService = new MenuItemService();

				Console.WriteLine("Enter item details");
				Console.WriteLine("Category:");
				var category = Console.ReadLine();


				var items = await menuItemService.GetByCategory(category);

				foreach (var item in items)
					Console.WriteLine($"{item.Id} {item.Name} {item.Price}");
			}

			else if (input == 6)
			{
				MenuItemService menuItemService = new MenuItemService();

				Console.WriteLine("Enter item details");
				Console.WriteLine("Min price:");
				var minPrice = decimal.Parse(Console.ReadLine());
				Console.WriteLine("Max price:");
				var maxPrice = decimal.Parse(Console.ReadLine());

				var items = await menuItemService.GetByPriceInterval(minPrice, maxPrice);

				foreach (var item in items)
					Console.WriteLine($"{item.Id} {item.Name} {item.Price}");
			}

			else if (input == 7)
			{
				MenuItemService menuItemService = new MenuItemService();

				Console.WriteLine("Enter item details");
				Console.WriteLine("Name:");
				var Name = Console.ReadLine();

				var items = await menuItemService.GetByName(Name);

				foreach (var item in items)
				{
					Console.WriteLine($"{item.Id} {item.Name} {item.Price}");
				}


			}

			else if (input == 0)
			{
				return;
			}
		}

		private async Task OrderService()
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

			if(input == 1)
			{
				Order order = new Order();
				Console.WriteLine("Enter order details");
				Console.WriteLine("Menu item id:");
				Guid menuItemId = Guid.Parse(Console.ReadLine());
				Console.WriteLine("Name:");
				var name = Console.ReadLine();
				Console.WriteLine("Price:");
				var price = decimal.Parse(Console.ReadLine());
				Console.WriteLine("Category:");
				var category = Console.ReadLine();

				MenuItem menuItem = new MenuItem();

				menuItem.Name = name!;
				menuItem.Price = price;
				menuItem.Category.Name = category!;

				MenuItemService menuItemService = new MenuItemService();

				Console.WriteLine("Count:");
				int count = int.Parse(Console.ReadLine());

				OrderService orderService = new OrderService();
				await orderService.AddOrder(menuItem, count);
			}
			else if(input == 2)
			{
				Order order = new Order();
				Console.WriteLine("Enter order details");
				Console.WriteLine("Id:");
				Guid id = Guid.Parse(Console.ReadLine());

				OrderService orderService = new OrderService();

				await orderService.RemoveOrder(id);
			}
			else if(input == 3)
			{
				OrderService orderService = new OrderService();
				var orders = await orderService.Orders();
				foreach (var order in orders)
					Console.WriteLine($"{order.Id} {order.TotalAmount} {order.Date}");
			}
			else if(input == 4)
			{
				OrderService orderService = new OrderService();
				Console.WriteLine("Enter order details");
				Console.WriteLine("Start date:");
				DateTime startDate = DateTime.Parse(Console.ReadLine());
				Console.WriteLine("End date:");
				DateTime endDate = DateTime.Parse(Console.ReadLine());
				var orders = await orderService.GetOrdersByDatesInterval(startDate, endDate);
				foreach (var order in orders)
					Console.WriteLine($"{order.Id} {order.TotalAmount} {order.Date}");
			}
			else if(input == 5)
			{
			    OrderService orderService = new OrderService();
				Console.WriteLine("Enter order details");
				Console.WriteLine("Min amount:");
				decimal minAmount = decimal.Parse(Console.ReadLine());
				decimal maxAmount = decimal.Parse(Console.ReadLine());
				var orders = await orderService.GetOrdersByPriceInterval(minAmount, maxAmount);
				foreach (var order in orders)
					Console.WriteLine($"{order.Id} {order.TotalAmount} {order.Date}");
			}
			else if(input == 6)
			{
			    OrderService orderService = new OrderService();
				Console.WriteLine("Enter order details");
				Console.WriteLine("Date:");
				DateTime date = DateTime.Parse(Console.ReadLine());
				var orders = await orderService.GetOrderByDate(date);
				foreach (var order in orders)
					Console.WriteLine($"{order.Id} {order.TotalAmount}  {order.Date}");
			}
			else if(input == 7)
			{
				OrderService orderService = new OrderService();
				Console.WriteLine("Enter order details");
				Console.WriteLine("Id:");
				Guid id = Guid.Parse(Console.ReadLine());
				Console.WriteLine(orderService.GetOrderByNo(id));
			}
			 else if (input == 0)
			 {
				 return;
			 }

		}

		
	}
}
