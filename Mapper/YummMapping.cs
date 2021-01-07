using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.DTOs;
using Yumm_Back_End.Models.DTOs.AddressDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Mapper
{
    public class YummMapping : Profile
    {
        public YummMapping()
        {
            CreateMap<Province, ProvinceDTO>().ReverseMap();
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<Commune, CommuneDTO>().ReverseMap();
            CreateMap<Neighbourhood, NeighbourhoodDTO>().ReverseMap();
            CreateMap<Street, StreetDTO>().ReverseMap();
            CreateMap<RestaurantType, RestaurantTypeDTO>().ReverseMap();
            CreateMap<Restaurant, _RestaurantDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();

            #region Products Mapping
            //Mapping for Products
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<CreateProductDTO, UpdateProductDTO>().ReverseMap();

            #endregion

        }
    }
}
