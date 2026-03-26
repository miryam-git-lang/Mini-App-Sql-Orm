using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierApp.BLL.Services;
using NtierApp.Core.Models;
using NtierApp.DAL.Concretes;
using NtierApp.DAL.Context;
using NtierApp.DAL.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static NtierApp.Core.Models.Enum;
using Enum = System.Enum;

namespace NtierApp.PL.Manager
{
	public class AppManager
	{

		public void Start()
		{
			while (true)
			{
				Console.WriteLine(" MAIN MENU ");
				Console.WriteLine("~~~~~~~~~~~~");
				Console.WriteLine("1 - Perform operations on the menu");
				Console.WriteLine("2 - Perform operations on the order");
				Console.WriteLine("0 - Exit");
				if (!int.TryParse(Console.ReadLine(), out var input))
				{
					Console.WriteLine("Invalid input. Please enter a valid number.");
					continue;
				}

				switch (input)
				{
					case 1:
						MenuService().Wait();
						break;

					case 2:
						OrderService().Wait();
						break;

					case 0:
						return;

					default:
						Console.WriteLine("Invalid option. Please try again.");
						break;
				}
			}
		}
		private async Task MenuService()
		{
			while (true)
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
				if (!int.TryParse(Console.ReadLine(), out var input))
				{
					Console.WriteLine("Invalid input. Please enter a valid number.");
					continue;
				}
				if (input == 1)
				{
					while (true)
					{
						Console.WriteLine("Enter item details");
						Console.WriteLine("Name:");
						var name = Console.ReadLine();

						if (name?.ToLower() == "q")
						{
							Console.WriteLine("Operation cancelled.");
							break;
						}

						Console.WriteLine("Price:");
						var priceInput = Console.ReadLine();

						if (priceInput?.ToLower() == "q")
						{
							Console.WriteLine("Operation cancelled.");
							break;
						}

						if (!decimal.TryParse(priceInput, out var price))
						{
							Console.WriteLine("Invalid price. Please enter a valid number.");
							continue;
						}

						bool categoryValid = false;
						Category selectedCategory = Category.Appetizer;

						while (!categoryValid)
						{
							Console.WriteLine("Category (Appetizer, Soup, Salad, MainCourse, Grill, Dessert, Drink):");
							var categoryInput = Console.ReadLine();

							if (categoryInput?.ToLower() == "q")
							{
								Console.WriteLine("Operation cancelled.");
								return;
							}

							if (Enum.TryParse<Category>(categoryInput, ignoreCase: true, out var category))
							{
								selectedCategory = category;
								categoryValid = true;
							}
							else
							{
								Console.WriteLine("Invalid category. Please try again.");
							}
						}

						MenuItem menuItem = new MenuItem();
						menuItem.Name = name!;
						menuItem.Price = price;
						menuItem.Category = selectedCategory;

						IRepository<MenuItem> MenuRepository = new Repository<MenuItem>(new AppDbContext());
						MenuItemService menuItemService = new MenuItemService(MenuRepository);
						try
						{
							await menuItemService.AddMenuItem(menuItem);
							Console.WriteLine("Item added successfully!");
						}
						catch (Exception ex)
						{
							Console.WriteLine($"Error adding item: {ex.Message}");
							if (ex.InnerException != null)
							{
								Console.WriteLine($"Inner error: {ex.InnerException.Message}");
							}
						}
						break;
					}
				}

				else if (input == 2)
				{
					MenuItem UpdatedMenuItem = new MenuItem();
					IRepository<MenuItem> MenuRepository = new Repository<MenuItem>(new AppDbContext());
					MenuItemService menuItemService = new MenuItemService(MenuRepository);

					Console.WriteLine("Available items:");
					var items = await menuItemService.MenuItems();
					foreach (var item in items)
						Console.WriteLine($"{item.Id} {item.Name} {item.Price}");

					Console.WriteLine("Enter item details");
					Console.WriteLine("Id:");
					try
					{
						Guid id = Guid.Parse(Console.ReadLine());
						Console.WriteLine("Make changes:");
						Console.WriteLine("   Name:");
						UpdatedMenuItem.Name = Console.ReadLine();
						Console.WriteLine("   Price:");
						UpdatedMenuItem.Price = decimal.Parse(Console.ReadLine());
						Console.WriteLine("   Category:");
						var category = Console.ReadLine();
						if (Enum.TryParse<Category>(category, ignoreCase: true, out var parsedCategory))
						{
							UpdatedMenuItem.Category = parsedCategory;
						}
						else
						{
							Console.WriteLine("Invalid category. Please try again.");
							continue;
						}
						await menuItemService.EditMenuItem(id, UpdatedMenuItem);
						Console.WriteLine("Item updated successfully!");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error updating item: {ex.Message}");
					}
				}

				else if (input == 3)
				{
					MenuItem UpdatedMenuItem = new MenuItem();
					IRepository<MenuItem> MenuRepository = new Repository<MenuItem>(new AppDbContext());
					MenuItemService menuItemService = new MenuItemService(MenuRepository);

					Console.WriteLine("Available items:");
					var items = await menuItemService.MenuItems();
					foreach (var item in items)
						Console.WriteLine($"{item.Id} {item.Name} {item.Price}");

					Console.WriteLine("Enter item details");
					Console.WriteLine("Id:");
					try
					{
						Guid id = Guid.Parse(Console.ReadLine());
						await menuItemService.RemoveMenuItem(id);
						Console.WriteLine("Item deleted successfully!");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error deleting item: {ex.Message}");
					}
				}

				else if (input == 4)
				{
					IRepository<MenuItem> MenuRepository = new Repository<MenuItem>(new AppDbContext());
					MenuItemService menuItemService = new MenuItemService(MenuRepository);

					var items = await menuItemService.MenuItems();

					foreach (var item in items)
						Console.WriteLine($"{item.Id} {item.Name} {item.Price}");
				}

				else if (input == 5)
				{
					IRepository<MenuItem> MenuRepository = new Repository<MenuItem>(new AppDbContext());
					MenuItemService menuItemService = new MenuItemService(MenuRepository);

					Console.WriteLine("Enter item details");
					Console.WriteLine("Category (Appetizer, Soup, Salad, MainCourse, Grill, Dessert, Drink):");
					var categoryInput = Console.ReadLine();

					if (System.Enum.TryParse<NtierApp.Core.Models.Enum.Category>(categoryInput, ignoreCase: true, out var category))
					{
						var items = await menuItemService.GetByCategory(category);

						foreach (var item in items)
							Console.WriteLine($"{item.Id} {item.Name} {item.Price}");
					}
					else
					{
						Console.WriteLine("Invalid category. Please enter a valid category.");
					}
				}

				else if (input == 6)
				{
					IRepository<MenuItem> MenuRepository = new Repository<MenuItem>(new AppDbContext());
					MenuItemService menuItemService = new MenuItemService(MenuRepository);

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
					IRepository<MenuItem> MenuRepository = new Repository<MenuItem>(new AppDbContext());
					MenuItemService menuItemService = new MenuItemService(MenuRepository);

					Console.WriteLine("Enter item details");
					Console.WriteLine("Name:");
					var Name = Console.ReadLine();

					var items = await menuItemService.GetByName(Name);

					foreach (var item in items)
						Console.WriteLine($"{item.Id} {item.Name} {item.Price}");

				}

				else if (input == 0)
					return;
			}
		}

		private async Task OrderService()
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
				if (!int.TryParse(Console.ReadLine(), out var input))
				{
					Console.WriteLine("Invalid input. Please enter a valid number.");
					continue;
				}

				if(input == 1)
				{
					try
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

						IRepository<MenuItem> menuItemRepository = new Repository<MenuItem>(new AppDbContext());
						MenuItemService menuItemService = new MenuItemService(menuItemRepository);

						Console.WriteLine("Count:");
						int count = int.Parse(Console.ReadLine());

						IRepository<Order> orderRepository = new Repository<Order>(new AppDbContext());
						OrderService orderService = new OrderService(orderRepository);
						await orderService.AddOrder(menuItem, count);
						Console.WriteLine("Order added successfully!");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error adding order: {ex.Message}");
					}
				}
				else if(input == 2)
				{
					try
					{
						Order order = new Order();
						Console.WriteLine("Enter order details");
						Console.WriteLine("Id:");
						Guid id = Guid.Parse(Console.ReadLine());

						IRepository<Order> orderRepository = new Repository<Order>(new AppDbContext());
						OrderService orderService = new OrderService(orderRepository);

						await orderService.RemoveOrder(id);
						Console.WriteLine("Order deleted successfully!");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error deleting order: {ex.Message}");
					}
				}
				else if(input == 3)
				{
					try
					{
						IRepository<Order> orderRepository = new Repository<Order>(new AppDbContext());
						OrderService orderService = new OrderService(orderRepository);

						var orders = await orderService.Orders();
						foreach (var order in orders)
							Console.WriteLine($"{order.Id} {order.TotalAmount} {order.Date}");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error retrieving orders: {ex.Message}");
					}
				}
				else if(input == 4)
				{
					try
					{
						IRepository<Order> orderRepository = new Repository<Order>(new AppDbContext());
						OrderService orderService = new OrderService(orderRepository);

						Console.WriteLine("Enter order details");
						Console.WriteLine("Start date:");
						DateTime startDate = DateTime.Parse(Console.ReadLine());
						Console.WriteLine("End date:");
						DateTime endDate = DateTime.Parse(Console.ReadLine());
						var orders = await orderService.GetOrdersByDatesInterval(startDate, endDate);
						foreach (var order in orders)
							Console.WriteLine($"{order.Id} {order.TotalAmount} {order.Date}");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error retrieving orders: {ex.Message}");
					}
				}
				else if(input == 5)
				{
					try
					{
						IRepository<Order> orderRepository = new Repository<Order>(new AppDbContext());
						OrderService orderService = new OrderService(orderRepository);

						Console.WriteLine("Enter order details");
						Console.WriteLine("Min amount:");
						decimal minAmount = decimal.Parse(Console.ReadLine());
						Console.WriteLine("Max amount:");
						decimal maxAmount = decimal.Parse(Console.ReadLine());
						var orders = await orderService.GetOrdersByPriceInterval(minAmount, maxAmount);
						foreach (var order in orders)
							Console.WriteLine($"{order.Id} {order.TotalAmount} {order.Date}");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error retrieving orders: {ex.Message}");
					}
				}
				else if(input == 6)
				{
					try
					{
						IRepository<Order> orderRepository = new Repository<Order>(new AppDbContext());
						OrderService orderService = new OrderService(orderRepository);

						Console.WriteLine("Enter order details");
						Console.WriteLine("Date:");
						DateTime date = DateTime.Parse(Console.ReadLine());
						var orders = await orderService.GetOrderByDate(date);
						foreach (var order in orders)
							Console.WriteLine($"{order.Id} {order.TotalAmount}  {order.Date}");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error retrieving orders: {ex.Message}");
					}
				}
				else if(input == 7)
				{
					try
					{
						IRepository<Order> orderRepository = new Repository<Order>(new AppDbContext());
						OrderService orderService = new OrderService(orderRepository);

						Console.WriteLine("Enter order details");
						Console.WriteLine("Id:");
						Guid id = Guid.Parse(Console.ReadLine());
						Console.WriteLine(orderService.GetOrderByNo(id));
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error retrieving order: {ex.Message}");
					}
				}
				else if (input == 0)
					return;
			}
		}
	}
}
