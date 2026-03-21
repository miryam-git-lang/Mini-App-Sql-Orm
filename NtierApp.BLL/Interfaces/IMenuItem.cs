using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.BLL.Interfaces
{
	public interface IMenuItem
	{
		public void MenuItems();
		public void AddMenuItem();
		public void RemoveMenuItem();
		public void EditMenuItem();
		public void GetByCategory();
		public void GetByPriceInterval();
		public void GetByName();



	}
}
