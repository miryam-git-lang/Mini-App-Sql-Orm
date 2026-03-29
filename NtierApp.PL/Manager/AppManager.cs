using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NtierApp.BLL.Dtos.OrderItemsDto;
using NtierApp.BLL.Dtos.MenuItemsDtos;
using NtierApp.BLL.Dtos.OrderDtos;
using NtierApp.BLL.Interfaces;
using NtierApp.BLL.Mappers;
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
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			serviceCollection.AddScoped<IMenuItem, MenuItemService>();
			serviceCollection.AddDbContext<AppDbContext>();
			serviceCollection.AddAutoMapper(opt => opt.AddProfile(new MapProfile()));
			serviceCollection.AddLogging();

			var serviceProvider = serviceCollection.BuildServiceProvider();
			var menuItemService = serviceProvider.GetRequiredService<IMenuItem>();

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

						var menuItemDto = new MenuItemCreateDto
						{
							Name = name!,
							Price = price,
							Category = selectedCategory
						};

						try
						{
							await menuItemService.AddMenuItem(menuItemDto);
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
						var updatedMenuItemDto = new MenuItemCreateDto
						{
							Name = UpdatedMenuItem.Name,
							Price = UpdatedMenuItem.Price,
							Category = UpdatedMenuItem.Category
						};
						await menuItemService.EditMenuItem(id, updatedMenuItemDto);
						Console.WriteLine("Item updated successfully!");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error updating item: {ex.Message}");
					}
				}

				else if (input == 3)
				{
					MenuItemCreateDto UpdatedMenuItem = new MenuItemCreateDto();

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

					var items = await menuItemService.MenuItems();

					PrintMenuItemsTable(items);
				}

				else if (input == 5)
				{
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
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			serviceCollection.AddScoped<IMenuItem, MenuItemService>();
			serviceCollection.AddScoped<IOrder, OrderService>();
			serviceCollection.AddDbContext<AppDbContext>();
			serviceCollection.AddAutoMapper(opt => opt.AddProfile(new MapProfile()));
			serviceCollection.AddLogging();

			var serviceProvider = serviceCollection.BuildServiceProvider();
			var menuItemService = serviceProvider.GetRequiredService<IMenuItem>();
			var orderService = serviceProvider.GetRequiredService<IOrder>();

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
						var items = await menuItemService.MenuItems();

						if (items == null || items.Count == 0)
						{
							WriteStatus("Menu is empty. Please add items first.", ConsoleColor.Yellow);
							continue;
						}

                    PrintAvailableMenuItems(items);

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

					var orderMenuItem = new MenuItem
					{
						Id = selectedItem.Id,
						Name = selectedItem.Name,
						Price = selectedItem.Price,
						Category = selectedItem.Category
					};

                   var order = await orderService.AddOrder(orderMenuItem, count);
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
						OrderCreateDto order = new OrderCreateDto();
						var orders = await orderService.Orders();
						await PopulateOrderItemsAsync(orders, serviceProvider);
						PrintOrdersTable(orders);
						Console.WriteLine("Enter order details");
						Console.WriteLine("Id:");
						Guid id = Guid.Parse(Console.ReadLine());

						await orderService.RemoveOrder(id);
						Console.WriteLine("Order deleted successfully!");

						var menuItemsAfterDeletion = await menuItemService.MenuItems();
						
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
						var orders = await orderService.Orders();
                       await PopulateOrderItemsAsync(orders, serviceProvider);
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
						Console.WriteLine("Enter order details");
						Console.WriteLine("Start date:");
						DateTime startDate = DateTime.Parse(Console.ReadLine());
						Console.WriteLine("End date:");
						DateTime endDate = DateTime.Parse(Console.ReadLine());
                        var orders = await orderService.GetOrdersByDatesInterval(startDate, endDate);
                       await PopulateOrderItemsAsync(orders, serviceProvider);
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
						Console.WriteLine("Enter order details");
						Console.WriteLine("Min amount:");
						decimal minAmount = decimal.Parse(Console.ReadLine());
						Console.WriteLine("Max amount:");
						decimal maxAmount = decimal.Parse(Console.ReadLine());
                        var orders = await orderService.GetOrdersByPriceInterval(minAmount, maxAmount);
                       await PopulateOrderItemsAsync(orders, serviceProvider);
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
						Console.WriteLine("Enter order details");
						Console.WriteLine("Date:");
                     var dateInput = Console.ReadLine();
						if (!DateTime.TryParse(dateInput, out var date))
						{
							Console.WriteLine("Invalid date. Please enter a valid date.");
							continue;
						}
						var orders = await orderService.GetOrderByDate(date);
						var filteredOrders = orders?.Where(o => o.Date.Date == date.Date).ToList() ?? new List<OrderReturnDto>();

						if (filteredOrders.Count == 0)
						{
							var allOrders = await orderService.Orders();
							filteredOrders = allOrders.Where(o => o.Date.Date == date.Date).ToList();
						}

						await PopulateOrderItemsAsync(filteredOrders, serviceProvider);
						PrintOrdersTable(filteredOrders);
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
                          var singleOrderList = new List<OrderReturnDto> { order };
							await PopulateOrderItemsAsync(singleOrderList, serviceProvider);
							PrintOrdersTable(singleOrderList);
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

     private static string FormatOrderItems(OrderReturnDto order)
		{
			if (order?.Items == null || order.Items.Count == 0)
				return "-";

			return string.Join(", ", order.Items.Select(oi =>
			{
				var name = string.IsNullOrWhiteSpace(oi.MenuItemName) ? "Item" : oi.MenuItemName;
				return $"{name} x{oi.Count}";
			}));
		}

	private static void PrintAvailableMenuItems(IEnumerable<MenuItemReturnDto> items)
	{
		Console.WriteLine("------------------------------");
		Console.WriteLine("Available menu items:");
		Console.WriteLine("------------------------------");

		foreach (var item in items ?? Enumerable.Empty<MenuItemReturnDto>())
			Console.WriteLine($"{item.Id} {item.Name} {FormatCurrency(item.Price)}");
		Console.WriteLine("------------------------------");
	}

		private static void PrintMenuItemsTable(IEnumerable<MenuItemReturnDto> items)
		{
			const string format = "{0,-36} | {1,-20} | {2,-12} | {3,15}";
			Console.WriteLine();
			Console.WriteLine(format, "ID", "Name", "Category", "Price");
			Console.WriteLine(new string('-', 90));
			foreach (var item in items)
				Console.WriteLine(format, item.Id, item.Name, item.Category, FormatCurrency(item.Price));
		}

     private static void PrintOrdersTable(IEnumerable<OrderReturnDto> orders)
		{
			const string format = "{0,-36} | {1,15} | {2,19} | {3}";
			Console.WriteLine();
			Console.WriteLine(format, "ID", "Total", "Date", "Items");
			Console.WriteLine(new string('-', 120));
          foreach (var order in orders ?? Enumerable.Empty<OrderReturnDto>())
			{
				var dateText = order.Date.ToString("yyyy-MM-dd HH:mm");
              Console.WriteLine(format, order.Id, FormatCurrency(order.TotalAmount), dateText, FormatOrderItems(order));
			}
		}

	private static async Task PopulateOrderItemsAsync(IEnumerable<OrderReturnDto>? orders, IServiceProvider serviceProvider)
	{
		if (orders == null)
			return;

		var orderList = orders.Where(o => o != null).ToList();
		if (orderList.Count == 0)
			return;

		var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
		var orderIds = orderList.Select(o => o.Id).Distinct().ToList();

		var orderItemsData = await dbContext.OrderItems
			.Where(oi => orderIds.Contains(oi.OrderId))
			.Select(oi => new
			{
				oi.OrderId,
				oi.Count,
				MenuItemName = oi.MenuItem.Name,
				Price = oi.MenuItem.Price
			})
			.ToListAsync();

		var lookup = orderItemsData
			.GroupBy(item => item.OrderId)
			.ToDictionary(
				g => g.Key,
				g => g.Select(item => new OrderItemReturnDto
				{
					MenuItemName = item.MenuItemName,
					Count = item.Count,
					Price = item.Price
				}).ToList());

		foreach (var order in orderList)
		{
			if (lookup.TryGetValue(order.Id, out var items))
			{
				order.Items = items;
			}
			else if (order.Items == null)
			{
				order.Items = new List<OrderItemReturnDto>();
			}
		}
	}
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