using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NtierApp.Core.Models;
using NtierApp.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.DAL.Context
{
	public class AppDbContext : DbContext
	{
		public DbSet<MenuItem> MenuItems { get; set; } = null!;
		public DbSet<OrderItem> OrderItems { get; set; } = null!;
		public DbSet<Order> Orders { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
				optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RestorauntDb;Trusted_Connection=True;TrustServerCertificate=True;");

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		}

		public override int SaveChanges()
		{
			var entries = ChangeTracker.Entries<AuditableEntity>().ToList();
			foreach(var entry in entries)
			{
				switch(entry.State)
				{
					case EntityState.Deleted:
						entry.Entity.IsDeleted = true;
						entry.Entity.DeletedAt = DateTime.Now;
						break;
					case EntityState.Modified:
						entry.Entity.UpdatedAt = DateTime.Now;
						break;
					case EntityState.Added:
						entry.Entity.CreatedAt = DateTime.Now;
						break;
				}
			}
			return base.SaveChanges();
		}
	}
}
