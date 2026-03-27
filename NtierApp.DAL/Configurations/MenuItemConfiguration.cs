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
	public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
	{
		public void Configure(EntityTypeBuilder<MenuItem> builder)
		{
			builder.ToTable("MenuItems");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id)
				.HasDefaultValueSql("NEWSEQUENTIALID()")
				.ValueGeneratedOnAdd();

			builder
				.Property(m => m.Name)
				.IsRequired()
				.HasMaxLength(50);
			builder
				.HasIndex(m => m.Name)
				.IsUnique();

			builder
				.Property(m => m.Price)
				.HasColumnType("decimal(18,2)");

			builder
				.Property(m => m.Category)
				.HasConversion<int>()
				.IsRequired();

			builder
				.Property(m => m.Number)
				.ValueGeneratedOnAdd()
				.UseIdentityColumn();
		}
	}
}
