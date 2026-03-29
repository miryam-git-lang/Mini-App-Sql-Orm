using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NtierApp.BLL.Dtos.MenuItemsDtos;
using NtierApp.BLL.Dtos.OrderDtos;
using NtierApp.BLL.Dtos.OrderItemsDto;
using NtierApp.Core.Models;

namespace NtierApp.BLL.Mappers
{
	public class MapProfile : Profile
	{
	    public MapProfile() 
		{
			CreateMap<MenuItemCreateDto, MenuItem>();
			CreateMap<OrderCreateDto, Order>();
			CreateMap<OrderItemCreateDto, OrderItem>();

			CreateMap<MenuItem, MenuItemReturnDto>();
			CreateMap<Order, OrderReturnDto>();
			CreateMap<OrderItem, OrderItemReturnDto>();
		}

	}
	
}
