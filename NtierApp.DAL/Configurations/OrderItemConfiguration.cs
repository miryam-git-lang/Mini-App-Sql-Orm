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
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.HasOne(m => m.MenuItem)
			.WithOne(oi => oi.OrderItem)
			.HasForeignKey<OrderItem>(oi => oi.MenuItemId);
		}
	}
}
