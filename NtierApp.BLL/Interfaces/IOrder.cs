using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.BLL.Interfaces
{
	public interface IOrder
	{
		public void Orders();
		public void AddOrder();
		public void RemoveOrder();
		public void GetOrdersByDatesInterval();
		public void GetOrderByDate();
		public void GetOrdersByPriceInterval();
		public void GetOrderByNo();

	}
}
