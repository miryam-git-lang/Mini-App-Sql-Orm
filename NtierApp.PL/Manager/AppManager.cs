using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierApp.BLL.Services;

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

		public void OrderService()
		{
			while (true)
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
				
				switch (input)
				{
					case 1:
						orderService.AddOrder();
						break;

					case 2:
						orderService.RemoveOrder();
						break;

					case 3:
						orderService.Orders();
						break;

					case 4:
						orderService.GetOrdersByDatesInterval();
						break;

					case 5:
						orderService.GetOrdersByPriceInterval();
						break;

					case 6:
						orderService.GetOrderByDate();
						break;

					case 7:
						orderService.GetOrderByNo();
						break;

					case 0:
						return;

				}

			}
		}

		public void MenuService()
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

			switch (input)
			{
				case 1:
					menuItemService.AddMenuItem();
					break;
				case 2:
					menuItemService.EditMenuItem();
					break;
				case 3:
					menuItemService.RemoveMenuItem();
					break;
				case 4:
					menuItemService.MenuItems();
					break;
				case 5:
					menuItemService.GetByCategory();
					break;
				case 6:
					menuItemService.GetByPriceInterval();
					break;
				case 7:
					menuItemService.GetByName();
					break;
				case 0:
					return;

			}
		}
	}
}
