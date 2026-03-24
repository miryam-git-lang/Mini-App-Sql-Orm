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
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasMany(m => m.MenuItems)
				   .WithOne(c => c.Category)
				   .HasForeignKey(c => c.CategoryId);

		}
	}
}
