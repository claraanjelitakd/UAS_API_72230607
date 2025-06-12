using AutoMapper;
using SimpleRESTApi.Data;
using SimpleRESTApi.Models;
using SimpleRESTApi.DTO;

namespace SimpleRESTApi.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category
            CreateMap<Category, CategoriesDTO>().ReverseMap();
            CreateMap<Category, CategoriesAddDTO>().ReverseMap();
            CreateMap<Category, CategoriesUpdateDTO>().ReverseMap();

            // Customer
            CreateMap<Customers, CustomersDTO>().ReverseMap();
            CreateMap<Customers, CustomersAddDTO>().ReverseMap();
            CreateMap<Customers, CustomersUpdateDTO>().ReverseMap();

            // Employee
            CreateMap<Employees, EmployeesDTO>().ReverseMap();
            CreateMap<Employees, EmployeeAddDTO>().ReverseMap();
            CreateMap<Employees, EmployeesUpdateDTO>().ReverseMap();

            // Product
            CreateMap<Products, ProductsDTO>().ReverseMap();
            CreateMap<Products, ProductAddDTO>().ReverseMap();
            CreateMap<Products, ProductsUpdateDTO>().ReverseMap();
            CreateMap<Products, ProductsWithCategoryDTO>().ReverseMap();

            // Sale
            CreateMap<Sales, SalesDTO>().ReverseMap();
            CreateMap<Sales, SalesAddDTO>().ReverseMap();
            CreateMap<Sales, SaleDetailsDTO>().ReverseMap();

            // SaleItem
            CreateMap<SaleItems, SaleItemsDTO>().ReverseMap();
            CreateMap<SaleItems, SaleItemsAddDTO>().ReverseMap();
            CreateMap<SaleItems, SaleItemsUpdateDTO>().ReverseMap();

            // SaleDetail
            CreateMap<SaleDetails, SaleDetailsDTO>().ReverseMap();

            CreateMap<AspUser, AspUserLoginDTO>().ReverseMap();
            CreateMap<AspUser, AspUserResetDTO>().ReverseMap();
            CreateMap<AspUser, AspUserResponseDTO>().ReverseMap();
            CreateMap<AspUser, AspUserDTO>().ReverseMap();
        }
    }
}
