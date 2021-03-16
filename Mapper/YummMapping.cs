 using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.Clients;
using Yumm_Back_End.Models.DTOs;
using Yumm_Back_End.Models.DTOs.AddressDTO;
using Yumm_Back_End.Models.DTOs.AddressDTO.Location;
using Yumm_Back_End.Models.DTOs.ClientDTO;
using Yumm_Back_End.Models.DTOs.OrderDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.MenuDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Models.Orders;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Mapper
{
    public class YummMapping : Profile
    {
        public YummMapping()
        {
            //var config = new MapperConfiguration(cfg => { 
            //    cfg.Cre
            //});

            #region User Mapping
            CreateMap<ApplicationUser,UserDTO>().ReverseMap();
                
            #endregion

            #region Address Mapping 
            CreateMap<Province, ProvinceDTO>().ReverseMap();
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<Commune, CommuneDTO>().ReverseMap();
            CreateMap<Neighbourhood, NeighbourhoodDTO>().ReverseMap();
            CreateMap<Street, StreetDTO>().ReverseMap();
            CreateMap<Location, LocationCreateDTO>().ReverseMap();
            CreateMap<Location, LocationDTO>().ReverseMap();

            #region User Location
            CreateMap<Location, CreateUserLocationDTO>().ReverseMap();
            CreateMap<Location, UsersLocations>().ReverseMap();
            CreateMap<UsersLocations, CreateUserLocationDTO>().ReverseMap();


            #endregion

            #endregion


            #region Restaurant Mappig
                
                CreateMap<RestaurantType, RestaurantTypeDTO>().ReverseMap();
                CreateMap<Restaurant, _RestaurantDTO>().ReverseMap();
                CreateMap<Category, CategoryDTO>().ReverseMap();
                CreateMap<Menu, CreateMenuDTO>().ReverseMap();
                CreateMap<MenuGroup, CreateMenuGroupDTO>().ReverseMap();
                CreateMap<MenuGroup, MenuGroupDTO>().ReverseMap();

            #endregion


            #region Products Mapping
            //Mapping for Products
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<CreateProductDTO, UpdateProductDTO>().ReverseMap();
            CreateMap<ProductExtras, CreateProductExtrasDTO>().ReverseMap();
            

            #endregion


            #region Client Mapping
            CreateMap<Client, ClientDTO>().ReverseMap();


            #endregion



            #region Order Mapping

            CreateMap<Order, CreateOrderDTO>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusDTO>().ReverseMap();
            CreateMap<OrdersProducts, CreateOrderProductsDTO>().ReverseMap();
            CreateMap<OrderProductExtras, CreateOrderProductExtrasDTO>().ReverseMap();
            


            #endregion
        }
    }
}
