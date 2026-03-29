using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierApp.Core.Models;

namespace NtierApp.DAL.Interfaces
{
	public interface IMenuItemRepository : IRepository<MenuItem>
	{
		void SearchMenuItem(string name);
	}
}
