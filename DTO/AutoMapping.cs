using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace UAS_POS_CLARA.DTO
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Models.Product, ProductDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();
            CreateMap<Models.SaleItems, SaleItemsDTO>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .ReverseMap();
            CreateMap<Models.Customer, CustomerDTO>()
                .ReverseMap();
            CreateMap<Models.Category, CategoryDTO>()
                .ReverseMap();
            CreateMap<Models.Sale, SaleDTO>().ReverseMap();
            CreateMap<Models.Sale, GetSaleDTO>().ReverseMap();
            CreateMap<Models.Sale, AddSaleDTO>().ReverseMap();
            CreateMap<Models.SaleItems, GetSaleItemsDTO>()
                .ForMember(dest => dest.Sale, opt => opt.MapFrom(src => src.Sale))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .ReverseMap();
            CreateMap<Models.Product, AddProductDTO>()
                .ReverseMap();
            CreateMap<Models.SaleItems, AddSaleItemsDTO>()
                .ForMember(dest => dest.SaleID, opt => opt.MapFrom(src => src.SaleID))
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
                .ReverseMap();
            CreateMap<Models.Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Models.Employee, AddEmployeeDTO>().ReverseMap();
            CreateMap<Models.AspUser, AspUserDTO>()
                .ReverseMap();
            CreateMap<Models.AspUser, LoginDTO>()
                .ReverseMap();
            CreateMap<Models.AspUser, ResetDTO>().ReverseMap();


        }
    }
}