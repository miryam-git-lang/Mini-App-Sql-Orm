using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierApp.Core.Models;
using NtierApp.DAL.Context;
using NtierApp.DAL.Interfaces;

namespace NtierApp.DAL.Concretes
{
	public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
	{
		public MenuItemRepository(AppDbContext context) : base(context)
		{
		}
		public void SearchMenuItem(string name)
		{
			
		}
	}
}
