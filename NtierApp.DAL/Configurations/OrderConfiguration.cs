using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NtierApp.Core.Models;

namespace NtierApp.DAL.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("Orders")
				.HasKey(o => o.Id);
			builder.Property(o => o.Id)
				.HasDefaultValueSql("NEWSEQUENTIALID()");

			builder.HasMany(o => o.OrderItems)
				   .WithOne(oi => oi.Order)
				   .HasForeignKey(oi => oi.OrderId);

			builder.Property(o => o.Date)
				.IsRequired()
				.HasDefaultValueSql("GETDATE()");

			builder
				.Property(o => o.TotalAmount)
				.HasColumnType("decimal(18,2)");
		}
	}
}
