using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NtierApp.Core.Models;

namespace NtierApp.DAL.Context
{
	public class AppDbContext : DbContext
	{
		public DbSet<MenuItem> MenuItems { get; set; } = null!;
		public DbSet<OrderItem> OrderItems { get; set; } = null!;
		public DbSet<Order> Orders { get; set; } = null!;
		public DbSet<Category> Categories { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
				optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RestorauntDb;Trusted_Connection=True;TrustServerCertificate=True;");

			base.OnConfiguring(optionsBuilder);
		}

	}
}
