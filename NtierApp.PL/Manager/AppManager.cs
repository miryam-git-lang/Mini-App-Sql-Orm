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
			Console.OutputEncoding = Encoding.UTF8;
			Console.InputEncoding = Encoding.UTF8;

			while (true)
			{
				DrawBanner("Main Menu");
				WriteOption(1, "Perform operations on the menu");
				WriteOption(2, "Perform operations on the order");
				WriteOption(0, "Exit");
				WriteDivider();

				if (!TryReadInt("Select option", out var input))
				{
					WriteStatus("Invalid input. Please enter a valid number.", ConsoleColor.Red);
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
				DrawBanner("Menu Service");
				WriteOption(1, "Add new item");
				WriteOption(2, "Edit item");
				WriteOption(3, "Delete item");
				WriteOption(4, "Show all items");
				WriteOption(5, "Show by category");
				WriteOption(6, "Show by price range");
				WriteOption(7, "Search by name");
				WriteOption(0, "Exit");
				WriteDivider();
				if (!TryReadInt("Select option", out var input))
				{
					WriteStatus("Invalid input. Please enter a valid number.", ConsoleColor.Red);
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
				PrintMenuItemsTable(items);

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
				PrintMenuItemsTable(items);

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

                 PrintMenuItemsTable(items);
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
					PrintMenuItemsTable(items);
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
				PrintMenuItemsTable(items);
				}

				else if (input == 7)
				{
					IRepository<MenuItem> MenuRepository = new Repository<MenuItem>(new AppDbContext());
					MenuItemService menuItemService = new MenuItemService(MenuRepository);

					Console.WriteLine("Enter item details");
					Console.WriteLine("Name:");
					var Name = Console.ReadLine();

                var items = await menuItemService.GetByName(Name);
				PrintMenuItemsTable(items);

				}

				else if (input == 0)
					return;
			}
		}

       private async Task OrderService()
		{
			while (true)
			{
				DrawBanner("Order Service");
				WriteOption(1, "Add new order");
				WriteOption(2, "Delete order");
				WriteOption(3, "Show all orders");
				WriteOption(4, "Orders by date range");
				WriteOption(5, "Orders by amount range");
				WriteOption(6, "Orders for a specific date");
				WriteOption(7, "Order details by ID");
				WriteOption(0, "Exit");
				WriteDivider();
				if (!TryReadInt("Select option", out var input))
				{
					WriteStatus("Invalid input. Please enter a valid number.", ConsoleColor.Red);
					continue;
				}

				if (input == 1)
				{
					try
					{
					IRepository<MenuItem> menuRepository = new Repository<MenuItem>(new AppDbContext());
					MenuItemService menuItemService = new MenuItemService(menuRepository);
					IRepository<Order> orderRepository = new Repository<Order>(new AppDbContext());
					OrderService orderService = new OrderService(orderRepository);

                  var items = await menuItemService.MenuItems();
					if (items == null || items.Count == 0)
					{
						WriteStatus("Menu is empty. Please add items first.", ConsoleColor.Yellow);
						continue;
					}

					Console.WriteLine("Available menu items:");
					PrintMenuItemsTable(items);

					var itemIdInput = Prompt("Enter menu item Id (Q to cancel)");
					if (IsCancelInput(itemIdInput))
					{
						WriteStatus("Order creation cancelled.", ConsoleColor.DarkYellow);
						continue;
					}

					if (!Guid.TryParse(itemIdInput, out var menuItemId))
					{
						WriteStatus("Invalid Id. Please try again.", ConsoleColor.Red);
						continue;
					}

					var selectedItem = items.FirstOrDefault(m => m.Id == menuItemId);
					if (selectedItem == null)
					{
						WriteStatus("Menu item not found. Please try again.", ConsoleColor.Red);
						continue;
					}

					if (!TryReadInt("Quantity", out var count) || count <= 0)
					{
						WriteStatus("Quantity must be a positive number.", ConsoleColor.Red);
						continue;
					}

                   var order = await orderService.AddOrder(selectedItem, count);
					WriteStatus($"Order created successfully! ID: {order.Id} · Total: {FormatCurrency(order.TotalAmount)}", ConsoleColor.Green);

					}
					catch (Exception ex)
					{
                 WriteStatus($"Error adding order: {ex.Message}", ConsoleColor.Red);
					}
				}
				else if (input == 2)
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
                else if (input == 3)
				{
					try
					{
						IRepository<Order> orderRepository = new Repository<Order>(new AppDbContext());
						OrderService orderService = new OrderService(orderRepository);

						var orders = await orderService.Orders();
                       PrintOrdersTable(orders);
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error retrieving orders: {ex.Message}");
					}
				}
				else if (input == 4)
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
						PrintOrdersTable(orders);
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error retrieving orders: {ex.Message}");
					}
				}
				else if (input == 5)
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
						PrintOrdersTable(orders);
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error retrieving orders: {ex.Message}");
					}
				}
				else if (input == 6)
				{
					try
					{
						IRepository<Order> orderRepository = new Repository<Order>(new AppDbContext());
						OrderService orderService = new OrderService(orderRepository);

						Console.WriteLine("Enter order details");
						Console.WriteLine("Date:");
						DateTime date = DateTime.Parse(Console.ReadLine());
                        var orders = await orderService.GetOrderByDate(date);
						PrintOrdersTable(orders);
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error retrieving orders: {ex.Message}");
					}
				}
                else if (input == 7)
				{
					try
					{
						IRepository<Order> orderRepository = new Repository<Order>(new AppDbContext());
						OrderService orderService = new OrderService(orderRepository);

						Console.WriteLine("Enter order details");
						Console.WriteLine("Id:");
						Guid id = Guid.Parse(Console.ReadLine());
                     var order = await orderService.GetOrderByNo(id);
						if (order == null)
						{
							Console.WriteLine("Order not found.");
						}
						else
						{
							PrintOrdersTable(new[] { order });
						}
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

     private const string ManatSymbol = "₼";

		private static string FormatCurrency(decimal amount)
		{
			return $"{amount:0.00} {ManatSymbol}";
		}

		private static string FormatOrderItems(Order order)
		{
			if (order?.OrderItems == null || order.OrderItems.Count == 0)
				return "-";

			return string.Join(", ", order.OrderItems.Select(oi =>
			{
				var name = oi.MenuItem?.Name ?? "Item";
				return $"{name} x{oi.Count}";
			}));
		}

        private static void PrintMenuItemsTable(IEnumerable<MenuItem> items)
		{
			const string format = "{0,-36} | {1,-20} | {2,-12} | {3,15}";
			Console.WriteLine();
			Console.WriteLine(format, "ID", "Name", "Category", "Price");
			Console.WriteLine(new string('-', 90));
			foreach (var item in items)
				Console.WriteLine(format, item.Id, item.Name, item.Category, FormatCurrency(item.Price));
		}

     private static void PrintOrdersTable(IEnumerable<Order> orders)
		{
			const string format = "{0,-36} | {1,15} | {2,19} | {3}";
			Console.WriteLine();
			Console.WriteLine(format, "ID", "Total", "Date", "Items");
			Console.WriteLine(new string('-', 120));
			foreach (var order in orders ?? Enumerable.Empty<Order>())
			{
				var dateText = order.Date.ToString("yyyy-MM-dd HH:mm");
				Console.WriteLine(format, order.Id, FormatCurrency(order.TotalAmount), dateText, FormatOrderItems(order));
			}
		}

		// Console helpers for consistent layout and input handling.
		private static void DrawBanner(string title)
		{
			Console.WriteLine();
			Console.WriteLine($"=== {title.ToUpperInvariant()} ===");
		}

		private static void WriteOption(int option, string description)
		{
			Console.WriteLine($"{option} - {description}");
		}

		private static void WriteDivider()
		{
			Console.WriteLine(new string('-', 30));
		}

		private static string Prompt(string label)
		{
			Console.Write($"{label}: ");
			return Console.ReadLine()?.Trim() ?? string.Empty;
		}

		private static bool TryReadInt(string label, out int value)
		{
			var input = Prompt(label);
			return int.TryParse(input, out value);
		}

		private static void WriteStatus(string message, ConsoleColor color)
		{
			Console.WriteLine(message);
		}

		private static bool IsCancelInput(string? input)
		{
			return string.Equals(input, "q", StringComparison.OrdinalIgnoreCase);
		}

	}

}